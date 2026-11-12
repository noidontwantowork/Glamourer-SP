using System.Reflection;

namespace ImSharp;

public static unsafe class ImSharpDebug
{
    private static void DrawField(Type type, object value, in Im.TableDisposable table, FieldInfo field)
    {
        using var fieldId = Im.Id.Push(field.Name);
        try
        {
            table.DrawColumn(field.Name);
            table.NextColumn();
            if (field.FieldType.IsGenericType)
                Im.Text($"{field.FieldType.Name}<{field.FieldType.GenericTypeArguments[0].Name}>");
            else
                Im.Text(field.FieldType.Name);
            var size = field.FieldType.IsGenericType
                ? sizeof(ImVector<int>)
                : field.FieldType.IsEnum
                    ? Marshal.SizeOf(field.FieldType.GetEnumUnderlyingType())
                    : Marshal.SizeOf(field.FieldType);
            table.DrawColumn($"{size}");
            table.NextColumn();
            if (field.FieldType.IsPointer || field.FieldType.IsFunctionPointer || field.FieldType.IsUnmanagedFunctionPointer)
            {
                Im.Text($"0x{(nint)Pointer.Unbox(field.GetValue(value)!):X}");
            }
            else if (field.FieldType.IsPrimitive)
            {
                Im.Text($"{field.GetValue(value)}");
            }
            else if (field.FieldType == typeof(ImVec2))
            {
                Im.Text($"{(ImVec2)field.GetValue(value)!}");
            }
            else if (field.FieldType == typeof(ImVec4))
            {
                Im.Text($"{(ImVec4)field.GetValue(value)!}");
            }
            else if (field.FieldType == typeof(ImRect))
            {
                var rect = (ImRect)field.GetValue(value)!;
                Im.Text($"{rect}");
            }
            else if (field.FieldType == typeof(ImBool))
            {
                Im.Text($"{(ImBool)field.GetValue(value)!}");
            }
            else if (field.FieldType == typeof(ImGuiId))
            {
                Im.Text($"{(ImGuiId)field.GetValue(value)!}");
            }
            else if (field.FieldType == typeof(Rgba32))
            {
                Im.Text($"{(Rgba32)field.GetValue(value)!}");
            }
            else if (field.FieldType.IsGenericType)
            {
                var vector = field.GetValue(value)!;
                var fields = field.FieldType.GetFields();
                var data = (nint)Pointer.Unbox(fields.First(f => f.Name is "Data").GetValue(vector)!);
                var count = (int)fields.First(f => f.Name is "Size").GetValue(vector)!;
                var capacity = (int)fields.First(f => f.Name is "Capacity").GetValue(vector)!;
                Im.Text($"0x{data:X} ({count}/{capacity})");
            }
            else if (field.FieldType.IsEnum)
            {
                var enumValue = Convert.ChangeType(field.GetValue(value), field.FieldType)!;
                if (Enum.IsDefined(field.FieldType, enumValue))
                    Im.Text($"{enumValue}");
                else if (field.FieldType.GetCustomAttribute<FlagsAttribute>() is null)
                    Im.Text($"{Convert.ChangeType(enumValue, field.FieldType.GetEnumUnderlyingType())}");
                else
                    Im.Text($"{Convert.ChangeType(enumValue, field.FieldType.GetEnumUnderlyingType()):X}");
            }
            else if (field.FieldType == typeof(Im.Native.ImGuiStyle.ColorArray))
            {
                var array = (Im.Native.ImGuiStyle.ColorArray)field.GetValue(value)!;
                using var colorTree = Im.Tree.Node("Expand"u8);
                if (colorTree)
                    using (Im.Indent())
                    {
                        foreach (var color in ImGuiColor.Values.SkipLast(1))
                        {
                            using var id = Im.Id.Push((int)color);
                            table.DrawColumn($"{color}");
                            table.DrawColumn($"{nameof(Vector4)}");
                            table.DrawColumn($"{sizeof(Vector4)}");
                            table.NextColumn();
                            Im.Color.Button(""u8, array[color]);
                        }
                    }
            }
            else if (field.FieldType.GetCustomAttribute<InlineArrayAttribute>() is { } inlineArray)
            {
                var array = field.GetValue(value);
                var subType = field.FieldType.GetProperties().FirstOrDefault(f => f.GetIndexParameters().Length > 0)?.GetMethod;
                if (subType is null)
                    return;

                for (var i = 0; i < inlineArray.Length; ++i)
                {
                    var arrayValue = subType.Invoke(array, BindingFlags.GetProperty, null, [i], CultureInfo.CurrentCulture);
                    Im.Text($"{arrayValue}, ");
                    Im.Line.NoSpacing();
                }
            }
            else
            {
                var subValue = field.GetValue(value)!;
                using var subTree = Im.Tree.Node("Expand"u8);
                if (subTree)
                {
                    using var indent = Im.Indent();
                    foreach (var subField in field.FieldType.GetFields())
                        DrawField(field.FieldType, subValue, table, subField);
                }
            }
        }
        catch
            (Exception ex)
        {
            Im.Text($"{ex.Message}");
            table.NextRow();
        }
    }

    public static void DrawAllFields<T>(T* pointer) where T : unmanaged
    {
        using var tree = Im.Tree.Node($"{typeof(T).Name}: 0x{(nint)pointer:X}");
        if (!tree)
            return;

        using var table = Im.Table.Begin("table"u8, 4, TableFlags.SizingFixedFit);
        if (!table)
            return;

        ref var obj = ref *pointer;
        foreach (var field in typeof(T).GetFields())
            DrawField(typeof(T), obj, table, field);
    }
}

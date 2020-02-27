using System;
using System.Collections.Generic;
using System.Text;

namespace Builder.Exercise
{
    internal class Field
    {
        public string Type, Name;
        public override string ToString()
        {
            return $"public {Type} {Name};";
        }
    }

    internal class Class
    {
        public string Name;
        public List<Field> Fields = new List<Field>();
        public Class() { }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"public class {Name}").AppendLine("{");
            foreach (var field in Fields)
            {
                sb.AppendLine($"  {field}");
            }
            sb.AppendLine("}");
            return sb.ToString();
        }
    }

    public class CodeBuilder
    {
        private Class theClass = new Class();
        public CodeBuilder(string name)
        {
            theClass.Name = name;
        }

        public CodeBuilder AddField(string name, string type)
        {
            theClass.Fields.Add(new Field { Name = name, Type = type });
            return this;
        }

        public override string ToString()
        {
            return theClass.ToString();
        }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
            Console.WriteLine(cb);
        }
    }

    //BAD CODE - there are some modifications needed in terms of oops but this solution can be extended far better
    //public class FieldBuilder
    //{
    //    public string type, name,className;
    //    private const int indentSize = 2;
    //    public string accessModifier = "public";
    //    public List<FieldBuilder> Fields = new List<FieldBuilder>();

    //    public FieldBuilder(string name, string type)
    //    {
    //        this.name = name;
    //        this.type = type;
    //    }

    //    public FieldBuilder()
    //    {
    //    }

    //    public override string ToString()
    //    {
    //        return ToStringImp(0);
    //    }

    //    private string ToStringImp(int indent)
    //    {
    //        var sb = new StringBuilder();
    //        var i = new string(' ', indentSize * indent);

    //        sb.Append(accessModifier);
    //        sb.Append(" ");
    //        sb.Append("class");
    //        sb.Append(" ");
    //        sb.Append(className);
    //        sb.Append("\n{");
    //        foreach (var field in Fields)
    //        {
    //            sb.AppendLine();
    //            sb.Append(new string(' ', indentSize * (indent + 1)));
    //            sb.Append(accessModifier);
    //            sb.Append(" ");
    //            sb.Append(field.type);
    //            sb.Append(" ");
    //            sb.Append(field.name);
    //            sb.Append(";");
    //        }
    //        sb.Append("\n}");
    //        return sb.ToString();
    //    }

    //}
    //public class CodeBuilder
    //{

    //    private readonly string Name;
    //    FieldBuilder root = new FieldBuilder();

    //    public CodeBuilder(string className)
    //    {
    //        this.Name = className;
    //        root.className = className;
    //    }

    //    public CodeBuilder AddField(string name,string type)
    //    {
    //        var e = new FieldBuilder(name, type);
    //        root.Fields.Add(e);
    //        return this;

    //    }
    //    public override string ToString()
    //    {
    //        return root.ToString();
    //    }

    //}
}

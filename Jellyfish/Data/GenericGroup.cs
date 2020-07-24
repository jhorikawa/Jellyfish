using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;

namespace Jellyfish.Data
{
    public class GenericGroup
    {
        private List<object> m_list = new List<object>();

        public GenericGroup()
        {
            m_list = new List<object>();
        }

        public GenericGroup(List<object> list)
        {
            if (list == null)
                m_list = new List<object>();
            m_list = list;
        }

        public GenericGroup Duplicate()
        {

            var dupList = new List<object>(m_list == null ? new List<object>() : m_list);
            var dupGenericGroup = new GenericGroup(dupList);

            return dupGenericGroup;
        }

        public List<object> GetList()
        {
            return m_list;
        }

    }

    public class GenericGroupGoo : GH_Goo<GenericGroup>
    {
        public GenericGroupGoo()
        {
            this.Value = new GenericGroup();
        }

        public GenericGroupGoo(GenericGroup genericGroup)
        {
            if (genericGroup == null)
                this.Value = new GenericGroup();
            this.Value = genericGroup;
        }

        public override bool IsValid
        {
            get
            {
                if(this.m_value == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public override string TypeName
        {
            get { return ("GenericGroup"); }
        }

        public override string TypeDescription
        {
            get { return ("Defines a generic group"); }
        }

        public override IGH_Goo Duplicate()
        {
            return new GenericGroupGoo(Value == null ? new GenericGroup() : Value.Duplicate());
        }

        public override string ToString()
        {
            return Value == null ? "" : Value.ToString();
        }


        public override bool CastTo<Q>(ref Q target)
        {
            //Cast to Generic Group.
            if (typeof(Q).IsAssignableFrom(typeof(GenericGroup)))
            {
                if (Value == null)
                    target = default(Q);
                else
                    target = (Q)(object)Value;
                return true;
            }

            target = default(Q);
            return false;
        }

        public override bool CastFrom(object source)
        {
            if (source == null) { return false; }

            if (typeof(GenericGroup).IsAssignableFrom(source.GetType()))
            {
                Value = (GenericGroup)source;
                return true;
            }

            return false;
        }
    }

    public class GenericGroupParameter : GH_PersistentParam<GenericGroupGoo>
    {
        public GenericGroupParameter() : base(new GH_InstanceDescription("Generic Group", "GenericGroup", "Generic group", "Jellyfish", "Data"))
        {
        }

        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                return null; //Todo, provide an icon.
            }
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("4F85E324-8B64-4C93-9C72-7B715F2ABC12"); }
        }

        protected override GH_GetterResult Prompt_Plural(ref List<GenericGroupGoo> values)
        {
            return GH_GetterResult.cancel;
        }
        protected override GH_GetterResult Prompt_Singular(ref GenericGroupGoo value)
        {
            return GH_GetterResult.cancel;
        }
    }
}

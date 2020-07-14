using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace Jellyfish
{
    public class JellyfishInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "Jellyfish";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "Package of utilities Junichiro Horikawa made for design efficiency.";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("2eda5f20-2ba4-44b0-95b4-bd759499ff6b");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "Junichiro Horikawa";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "twitter: @jhorikawa_err";
            }
        }
    }
}

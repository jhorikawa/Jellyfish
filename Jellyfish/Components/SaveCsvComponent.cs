using System;
using System.IO;
using System.Collections.Generic;

using Grasshopper.Kernel.Data;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;

namespace Jellyfish.Components
{
    public class SaveCsvComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the SaveCsvComponent class.
        /// </summary>
        public SaveCsvComponent()
          : base("Save CSV", "SaveCSV",
              "Save CSV file from text input.",
              "Jellyfish", "File")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Header", "H", "Header row texts.", GH_ParamAccess.list);
            pManager.AddTextParameter("Text", "T", "Text to save.", GH_ParamAccess.tree);
            Param_FilePath param = new Param_FilePath();
            pManager.AddParameter(param, "File", "F", "File path to save to.", GH_ParamAccess.item);
            pManager.AddTextParameter("Separator", "S", "Separator for csv text.", GH_ParamAccess.item, ",");
            pManager.AddBooleanParameter("Save", "S", "True to save.", GH_ParamAccess.item, false);

            pManager[0].Optional = true;
            pManager[2].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddTextParameter("CSV Text", "C", "Created CSV text.", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<string> headers = new List<string>();
            DA.GetDataList(0, headers);
            if (headers == null) headers = new List<string>();

            GH_Structure<GH_String> textTree;
            if (!DA.GetDataTree(1, out textTree)) return;

            string path = null;
            DA.GetData(2, ref path);

            string separator = null;
            if (!DA.GetData(3, ref separator)) return;

            bool save = false;
            if (!DA.GetData(4, ref save)) return;

            string csvText = "";

                
            if (headers.Count > 0)
            {
                for (int i = 0; i < headers.Count; i++)
                {
                    var header = headers[i];
                    csvText += "\"" + header + "\"";
                    if (i < headers.Count - 1)
                    {
                        csvText += separator;
                    }
                }
                csvText += Environment.NewLine;
            }


            for (int i = 0; i < textTree.Branches.Count; i++)
            {
                var branch = textTree.Branches[i];
                for(int n=0; n < branch.Count; n++)
                {
                    var txt = branch[n];
                    csvText += "\"" + txt + "\"";

                    if(n < branch.Count -1)
                    {
                        csvText += separator;
                    }
                }

                if(i < textTree.Branches.Count - 1)
                {
                    csvText += Environment.NewLine;
                }
            }

            if (save)
            {
                try
                {
                    var dir = Path.GetDirectoryName(path);
                    Directory.CreateDirectory(dir);
                    File.WriteAllText(path, csvText);
                }catch(Exception e)
                {

                }
            }

            DA.SetData(0, csvText);
        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("b02c6cb3-8368-4af5-a854-30ad3e5b387c"); }
        }
    }
}
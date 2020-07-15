using System;
using System.IO;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;

namespace Jellyfish.Components
{
    public class SaveTextComponent : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the WriteTextComponent class.
        /// </summary>
        public SaveTextComponent()
          : base("Save Text", "SaveText",
              "Save text file with input text.",
              "Jellyfish", "File")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Text", "T", "Text to save.", GH_ParamAccess.list);
            Param_FilePath param = new Param_FilePath();
            pManager.AddParameter(param, "File", "F", "File path to save to.", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Save", "S", "True to save.", GH_ParamAccess.item, false);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<string> texts = new List<string>();
            if (!DA.GetDataList(0, texts)) return;
            string path = null;
            if (!DA.GetData(1, ref path)) return;
            bool save = false;
            if (!DA.GetData(2, ref save)) return;

            if (save)
            {
                var dir = Path.GetDirectoryName(path);
                Directory.CreateDirectory(dir);

                string allText = "";
                for (int i = 0; i < texts.Count; i++)
                {
                    var txt = texts[i];
                    allText += txt;
                    if (i < texts.Count - 1)
                    {
                        allText += Environment.NewLine;
                    }

                }

                File.WriteAllText(path, allText);
            }
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
            get { return new Guid("b34fc336-d3e9-4b53-8008-658a70a12f5c"); }
        }
    }
}
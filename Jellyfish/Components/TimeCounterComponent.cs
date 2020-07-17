using System;
using System.Diagnostics;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Jellyfish.Components
{
    public class TimeCounterComponent : GH_Component
    {
        private Stopwatch stopwatch = new Stopwatch();
        private long secondVal = 0;
        private long millisecVal = 0;
        private long tickVal = 0;
        private long prevTickCount = 0;
        
        /// <summary>
        /// Initializes a new instance of the CounterComponent class.
        /// </summary>
        public TimeCounterComponent()
          : base("Time Counter", "TimeCounter",
              "Time Counter.",
              "Jellyfish", "Utility")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("Reset", "R", "Reset counter.", GH_ParamAccess.item, false);
            pManager.AddBooleanParameter("Start", "S", "Run counter.", GH_ParamAccess.item, false);
            pManager.AddIntegerParameter("Tick Step", "T", "Step millisecond value for elapsed tick.", GH_ParamAccess.item, 1);
            
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Second", "S", "Elapsed second.", GH_ParamAccess.item);
            pManager.AddGenericParameter("Millisecond", "M", "Elapsed millisecond.", GH_ParamAccess.item);
            pManager.AddGenericParameter("Tick", "T", "Elapsed tick.", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            bool reset = false;
            bool start = false;
            int tickStep = 0;
            if (!DA.GetData(0, ref reset)) return;
            if (!DA.GetData(1, ref start)) return;
            if (!DA.GetData(2, ref tickStep)) return;

            tickStep = Math.Max(1, tickStep);

            if (reset)
            {
                secondVal = 0;
                millisecVal = 0;
                tickVal = 0;
                prevTickCount = 0;
                stopwatch.Reset();
            }

            if (start)
            {
                stopwatch.Stop();
                secondVal = (long)Math.Floor(stopwatch.ElapsedMilliseconds / 1000.0);
                millisecVal = stopwatch.ElapsedMilliseconds;

                long tempTickCount = (millisecVal / tickStep);
                if (prevTickCount != tempTickCount)
                {
                    tickVal++;
                    prevTickCount = tempTickCount;
                }

                stopwatch.Start();

                this.ExpireSolution(true);
            }

            DA.SetData(0, secondVal);
            DA.SetData(1, millisecVal);
            DA.SetData(2, tickVal);
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
            get { return new Guid("3b6a3562-db8f-42ca-b3a4-0596420433c9"); }
        }
    }
}
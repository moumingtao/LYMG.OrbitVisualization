using JsonDiffPatchDotNet;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LYMG.RealTimeView
{
    public abstract class View
    {
        internal int Version;
        public JToken LastResponse { get; private set; }
        ViewContext Context;
        public string GroupName;

        JsonDiffPatch jdp;
        protected JsonDiffPatch JsonDiffPatch
        {
            get
            {
                if (jdp == null)
                    jdp = new JsonDiffPatch();
                return jdp;
            }
            set => jdp = value;
        }

        internal protected  abstract JToken Render();

        internal void Load(ViewContext context, string groupName)
        {
            this.Context = context;
            this.GroupName = groupName;
            this.LastResponse = Render();
        }

        public Task Diff()
        {
            var res = Render();
            var diff = JsonDiffPatch.Diff(LastResponse, res);
            if (diff == null) return Task.CompletedTask;
            LastResponse = res;
            Version++;
            return Context.NotifyDiffAsync(GroupName, Version, diff); ;
        }


    }
}

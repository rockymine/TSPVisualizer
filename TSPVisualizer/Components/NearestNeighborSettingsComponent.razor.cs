using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TSPVisualizer.Components {
    public partial class NearestNeighborSettingsComponent : ComponentBase {
        private int startNode;
        [Parameter]
        public int StartNode {
            get => startNode;
            set {
                if (value == startNode)
                    return;

                startNode = value;
                StartNodeChanged.InvokeAsync(value);
            }
        }
        [Parameter]
        public EventCallback<int> StartNodeChanged { get; set; }
    }
}

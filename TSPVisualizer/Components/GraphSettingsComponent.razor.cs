using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TSPVisualizer.Components {
    public partial class GraphSettingsComponent : ComponentBase {
        private int maxX;
        [Parameter]
        public int MaxX {
            get => maxX;
            set {
                if (value == maxX)
                    return;

                maxX = value;
                MaxXChanged.InvokeAsync(value);
            }
        }
        [Parameter]
        public EventCallback<int> MaxXChanged { get; set; }

        private int maxY;
        [Parameter]
        public int MaxY {
            get => maxY;
            set {
                if (value == maxY)
                    return;

                maxY = value;
                MaxYChanged.InvokeAsync(value);
            }
        }
        [Parameter]
        public EventCallback<int> MaxYChanged { get; set; }

        private int nodeCount;
        [Parameter]
        public int NodeCount {
            get => nodeCount;
            set {
                if (value == nodeCount)
                    return;

                nodeCount = value;
                NodeCountChanged.InvokeAsync(value);
            }
        }
        [Parameter]
        public EventCallback<int> NodeCountChanged { get; set; }
    }
}

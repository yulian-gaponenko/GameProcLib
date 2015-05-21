using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GamesProcLibVisualizer {
    interface IField {
        /// <returns>true if any changes to field were made</returns>
        bool TryDoMove(double xPx, double yPx);
        bool LetAiPlayersDoMove();
        void Draw(UIElementCollection fieldCanvas);

        GamesWindow.GameEndEvent GameEndAction { get; set; }
    }
}

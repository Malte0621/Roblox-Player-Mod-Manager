using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobloxPlayerModManager.Forms
{
    public partial class LoadingForm : Form
    {
        public LoadingForm(string text, ProgressBarStyle style, int? maximumProgress)
        {
            InitializeComponent();

            loadingTextLabel.Text = text;
            loadingProgressBar.Style = style;

            if (style != ProgressBarStyle.Marquee)
            {
                loadingProgressBar.Maximum = (int)maximumProgress;
            }
        }

        public void SetProgress(int progress)
        {
            if (loadingProgressBar.Style != ProgressBarStyle.Marquee)
            {
                loadingProgressBar.Value = progress;
            }
        }

        public void SetMaximumProgress(int maximumProgress)
        {
            if (loadingProgressBar.Style != ProgressBarStyle.Marquee)
            {
                loadingProgressBar.Maximum = maximumProgress;
            }
        }

        public void incrementProgress(int incrementNum)
        {
            if (loadingProgressBar.Style != ProgressBarStyle.Marquee)
            {
                loadingProgressBar.Increment(incrementNum);
            }
        }

        public void SetText(string text)
        {
            loadingTextLabel.Text = text;
        }


    }
}

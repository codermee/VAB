using System;
using Microsoft.Phone.Tasks;

namespace Vab.Views
{
    public partial class AboutView
    {
        public AboutView()
        {
            InitializeComponent();
        }

        private void OnApplicationBarEmailIconButtonClick(object sender, EventArgs e)
        {
            var emailComposeTask = new EmailComposeTask
                    {
                        Subject = "Ang. Vård av barn",
                        To = "codermee@hotmail.com"
                    };

            emailComposeTask.Show();
        }

        private void OnApplicationBarReviewIconButtonClick(object sender, EventArgs e)
        {
            var marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();
        }
    }
}
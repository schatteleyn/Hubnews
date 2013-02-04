using System;
using System.IO;
using System.Net;
using System.ServiceModel.Syndication;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

namespace hubnews
{
    public class RetrieveRSS
    {
        private MainPage mainPage;
        private String url;
        private ListBox listBox;

        public RetrieveRSS(MainPage mainPage, String url, ListBox listBox)
        {
            this.mainPage = mainPage;
            this.url = url;
            this.listBox = listBox;
            web_client();
        }

        private void web_client()
        {
            WebClient web_client = new WebClient();
            web_client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadStringCompleted);
            web_client.DownloadStringAsync(new System.Uri(this.url));
        }

        private void DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            UpdateFeedList(e.Result);
        }

        private void UpdateFeedList(string feedXML)
        {
            StringReader stringReader = new StringReader(feedXML);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            SyndicationFeed feed = SyndicationFeed.Load(xmlReader);

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                listBox.ItemsSource = feed.Items;
            });
        }

        public ListBox getListBox()
        {
            return listBox;
        }
    }
}

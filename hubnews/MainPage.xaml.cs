using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using System.ServiceModel.Syndication;

namespace hubnews
{
    public partial class MainPage : PhoneApplicationPage
    {
        private RetrieveRSS leFigaro;
        private RetrieveRSS leMonde;
        private RetrieveRSS leParisien;

        public MainPage()
        {
            InitializeComponent();
            init_rss();
        }

        public void init_rss()
        {
            progressBar.IsIndeterminate = true;

            //LE FIGARO
            leFigaro = new RetrieveRSS(this, "http://feeds.lefigaro.fr/c/32266/f/438191/index.rss", listBoxFigaro);

            //LE MONDE
            leMonde = new RetrieveRSS(this, "http://rss.lemonde.fr/c/205/f/3050/index.rss", listBoxLeMonde);

            //LE PARISIEN
            leParisien = new RetrieveRSS(this, "http://rss.leparisien.fr/leparisien/rss/une.xml", listBoxLeParisien);

            progressBar.IsIndeterminate = false;
        }

        private void ImgRefresh_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            init_rss();
            news_home();
        }

        private void ImgAbout_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
        }

        public void news_home()
        {
            SyndicationItem sItem;
            ListBox listBox;

            //LE FIGARO
            listBox = leFigaro.getListBox();
            sItem = (SyndicationItem)listBox.Items[0];
            textBlock_news1.Text = sItem.Title.Text;
            sItem = (SyndicationItem)listBox.Items[1];
            textBlock_news2.Text = sItem.Title.Text;

            //LE MONDE
            listBox = leMonde.getListBox();
            sItem = (SyndicationItem)listBox.Items[0];
            textBlock_news3.Text = sItem.Title.Text;
            sItem = (SyndicationItem)listBox.Items[1];
            textBlock_news4.Text = sItem.Title.Text;

            //LE PARISIEN
            listBox = leParisien.getListBox();
            sItem = (SyndicationItem)listBox.Items[0];
            textBlock_news5.Text = sItem.Title.Text;
            sItem = (SyndicationItem)listBox.Items[1];
            textBlock_news6.Text = sItem.Title.Text;
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;

            if (listBox != null && listBox.SelectedItem != null)
            {
                SyndicationItem sItem = (SyndicationItem)listBox.SelectedItem;

                if (sItem.Links.Count > 0)
                {
                    //En ATTENTE
                    String title = sItem.Title.Text;
                    DateTime publishDate = sItem.PublishDate.DateTime;
                    String url = sItem.Links.FirstOrDefault().Uri.ToString();

                    //Remove HTML tags (& useless string for LE FIGARO)
                    String descFixed = Regex.Replace(sItem.Summary.Text, "<[^>]+>", string.Empty);

                    if(descFixed.Contains("Articles en rapport"))
                    {
                        int index = descFixed.IndexOf("Articles en rapport");
                        String temp = descFixed.Substring(index);
                        descFixed = descFixed.Replace(temp, "");  
                    }

                    NavigationService.Navigate(new Uri("/NewsDetails.xaml?title=" + title
                                                                                  + "&publishDate=" + publishDate
                                                                                  + "&url=" + url
                                                                                  + "&desc=" + descFixed, UriKind.Relative));
                }
            }
        }

    }
}

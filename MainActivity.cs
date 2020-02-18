using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic;
using Android.Webkit;
using System;

namespace lab12
{
    [Activity(Label = "SQLiteDB", MainLauncher = true)]
    public class MainActivity : Activity
    {
        Button hb1Go;
        TextView txtURL;
        WebView webView;
        ListView lstViewData;
        List<Person> listSource = new List<Person>();
        Database db;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            showmain();
            //Create Database
           
        }
        private void showmain()
        {
            SetContentView(Resource.Layout.activity_main);
            hb1Go = FindViewById<Button>(Resource.Id.button1);
            webView = FindViewById<WebView>(Resource.Id.webView1);
            webView.SetWebViewClient(new WebViewClientClass());
            WebSettings webSettings = webView.Settings;
            webSettings.JavaScriptEnabled = true;
            Button dbutton = FindViewById<Button>(Resource.Id.button2);
            dbutton.Click += delegate
            {
                showDevelop();
            };

            hb1Go.Click += hb1Go_Click;
            db = new Database();
            db.createDatabase();
            lstViewData = FindViewById<ListView>(Resource.Id.listView);

            var edtName = FindViewById<EditText>(Resource.Id.edtName);
            var edtDepart = FindViewById<EditText>(Resource.Id.edtDepart);

            var edtEmail = FindViewById<EditText>(Resource.Id.edtEmail);
            var btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            var btnEdit = FindViewById<Button>(Resource.Id.btnEdit);
            var btnRemove = FindViewById<Button>(Resource.Id.btnRemove);
            //Load Data
            LoadData();
            //Event
            btnAdd.Click += delegate
            {
                Person person = new Person()
                {
                    Name = edtName.Text,
                    Price = decimal.Parse(edtDepart.Text),
                    Qulity = int.Parse(edtEmail.Text)
                };
                db.insertIntoTable(person);
                LoadData();
            };
            btnEdit.Click += delegate
            {
                Person person = new Person()
                {
                    Id = int.Parse(edtName.Tag.ToString()),
                    Name = edtName.Text,
                    Price = decimal.Parse(edtDepart.Text),
                    Qulity = int.Parse(edtEmail.Text)
                };
                db.updateTable(person);
                LoadData();
            };
            btnRemove.Click += delegate
            {
                Person person = new Person()
                {
                    Id = int.Parse(edtName.Tag.ToString()),
                    Name = edtName.Text,
                    Price = decimal.Parse(edtDepart.Text),
                    Qulity = int.Parse(edtEmail.Text)
                };
                db.removeTable(person);
                LoadData();
            };
            lstViewData.ItemClick += (s, e) =>
            {
                //Set Backround for selected item
                for (int i = 0; i < lstViewData.Count; i++)
                {
                    if (e.Position == i)

                        lstViewData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Blue);
                    else

                        lstViewData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Transparent);
                }
                //Binding Data
                var txtId = e.View.FindViewById<TextView>(Resource.Id.txtView_ID);
                var txtName =
               e.View.FindViewById<TextView>(Resource.Id.txtView_Name);
                var txtDepart =
               e.View.FindViewById<TextView>(Resource.Id.txtView_Depart);
                var txtEmail =
               e.View.FindViewById<TextView>(Resource.Id.txtView_Email);
                edtEmail.Text = txtName.Text;
                //edtName.Tag = e.Id;
                edtName.Tag = e.Id;
                edtName.Text = txtName.Text;
                edtDepart.Text = txtDepart.Text;
                edtEmail.Text = txtEmail.Text;
            };
        }
        private void showDevelop()
        {
            SetContentView(Resource.Layout.Develop);
            Button p2button = FindViewById<Button>(Resource.Id.db1);
            p2button.Click += delegate
            {
                showmain();
            };
        }
        private void hb1Go_Click(object sender, EventArgs e)
        {
            webView.LoadUrl("https://shopee.co.th/?gclid=Cj0KCQiAkKnyBRDwARIsALtxe7i_WCn-QdzhexpRK271OzoYoc0NPcF2TfvCkXd4HPAUNYdTMy_W5RIaAmgVEALw_wcB");
        }

        private void LoadData()
        {
            listSource = db.selectTable();
            var adapter = new ListViewAdapter(this, listSource);
            lstViewData.Adapter = adapter;
        }
    }

    internal class WebViewClientClass : WebViewClient
    {
        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {
            view.LoadUrl(url);
            return true;
        }
    }
}
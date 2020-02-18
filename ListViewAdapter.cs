using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace lab12
{
    public class ViewHolder : Java.Lang.Object
    {
        public TextView txtId { get; set; }
        public TextView txtName { get; set; }
        public TextView txtDepartment { get; set; }
        public TextView txtEmail { get; set; }
    }
    public class ListViewAdapter : BaseAdapter
    {
        private Activity activity;
        private List<Person> listPerson;
        public ListViewAdapter(Activity activity, List<Person> listPerson)
        {
            this.activity = activity;
            this.listPerson = listPerson;
        }
        public override int Count
        {
            get { return listPerson.Count; }
        }
        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }
        public override long GetItemId(int position)
        {
            return listPerson[position].Id;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ??
           activity.LayoutInflater.Inflate(Resource.Layout.list_view, parent, false);
            var txtID = view.FindViewById<TextView>(Resource.Id.txtView_ID);
            var txtName = view.FindViewById<TextView>(Resource.Id.txtView_Name);
            var txtDepart = view.FindViewById<TextView>(Resource.Id.txtView_Depart);
            var txtEmail = view.FindViewById<TextView>(Resource.Id.txtView_Email);
            txtID.Text = Convert.ToString(listPerson[position].Id);
            txtName.Text = listPerson[position].Name;
            txtDepart.Text = Convert.ToString(listPerson[position].Price);
            txtEmail.Text = Convert.ToString(listPerson[position].Qulity);
            return view;
        }
    }
}
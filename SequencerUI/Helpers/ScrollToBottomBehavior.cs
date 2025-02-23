using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SequencerUI.Helpers
{
    public class ScrollToBottomBehavior : Behavior<ListView>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            if (AssociatedObject.ItemsSource is INotifyCollectionChanged collection)
            {
                collection.CollectionChanged += Collection_CollectionChanged;
            }
        }

        private void Collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var listView = AssociatedObject;
                if (listView.Items.Count > 0)
                {
                    var lastItem = listView.Items[listView.Items.Count - 1];
                    listView.ScrollIntoView(lastItem);
                }
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (AssociatedObject.ItemsSource is INotifyCollectionChanged collection)
            {
                collection.CollectionChanged -= Collection_CollectionChanged;
            }
        }
    }
}

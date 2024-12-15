using Caliburn.Micro;
using System.Collections.Generic;

namespace SequenceBuilderUI.Helpers
{
    public class BindableCollectionHelper
    {
        /// <summary>
        /// Generic methods supporting transformation simple List<T> to BinableCollection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">Listo of generic object</param>
        /// <returns></returns>
        public static BindableCollection<T> ListToBindableCollection<T>(List<T> list)
        {
            BindableCollection<T> bindableList = new BindableCollection<T>();
            foreach (T o in list)
            {
                bindableList.Add(o);
            }
            return bindableList;
        }
    }
}

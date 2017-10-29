using readerLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace readerLib
{
    public class Factory
    {
        public Factory(IView view)
        {
            this.view = view;
        }
        public static Factory GetInstance(IView view)
        {
            if (instance == null)
            {
                instance = new Factory(view);
            }
            return instance;
        }
        public IView GetView()
        {
            return view;
        }
        private static Factory instance = null;
        private IView view;

    }
}

using UASKI.StaticModels;

namespace UASKI.Models
{
    public abstract class BasePage
    {
        public int Index { get; private set; }
        protected abstract void Show();
        public abstract void Clear();

        public void Init(bool IsOpen = true)
        {
            var form = SystemData.Form;
            form.tabControl1.SelectedIndex = Index;
            form.Menu_Step2.Enabled = false;
            SystemData.Pages.Clear();
            SystemData.Index = Index;

            if(IsOpen)
            {
                Show();
            }
        }

        public BasePage(int index)
        {
            Index = index;
        }

    }
}

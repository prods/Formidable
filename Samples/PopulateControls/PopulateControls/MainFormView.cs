using Formidable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PopulateControls.Repositories;

namespace PopulateControls
{
    /// <summary>
    /// Main Form View
    /// </summary>
    public class MainFormView : FormViewBase<MainFormViewModel>
    {
        private MemberRepository _memberRepository;

        public MainFormView() : base()
        {   
        }

        protected override void Initialize()
        {
            this._memberRepository = new MemberRepository();
        }

        protected override void InitializeOnDesignMode()
        {
            
        }

        public void LoadMembers()
        {
            if (this.ViewModel.GetMemberNumberInt() == 0)
            {
                throw new Exception("No members number was provided.");
            }
            this.ViewModel.Members = this._memberRepository.GetMembers(this.ViewModel.GetMemberNumberInt());
        }
    }
}

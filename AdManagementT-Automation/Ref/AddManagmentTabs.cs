using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdManagementT_Automation.Ref
{
   public enum AM_MainTab { Inventory,Proposals,Insertion_Orders,Advertisments,Reports,Admin}
   public enum AM_Sub_Inventory { Inventory,Wait_List }
   public  enum AM_Sub_Proposlas { All_Proposal, Create_Proposal }
   public enum AM_Sub_Insertion_Orders { All_Order, Create_Order,Edit_Order }
   public enum AM_Sub_Admin { Pages,Rate_Cards,Redis,Audit,Renewal,AddGroups }



}

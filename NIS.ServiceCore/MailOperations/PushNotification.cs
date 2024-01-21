using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NIS.ServiceCore.MailOperations
{
    class PushNotification
    {
        //private Notify _parent;
        
        //public PushNotification(Notify Parent)
        //{
        //    _parent = Parent;
        //}

        public SendNotificationResultType ProcessNotification(SendNotificationResponseType SendNotification)
        {          
            SendNotificationResultType result = new SendNotificationResultType(); // Notification's result
            bool unsubscribe = false; 

            ResponseMessageType[] responseMessages = SendNotification.ResponseMessages.Items; // Response messages

            foreach (ResponseMessageType responseMessage in responseMessages)
            {
                if (responseMessage.ResponseCode != ResponseCodeType.NoError)
                {                    
                    result.SubscriptionStatus = SubscriptionStatusType.Unsubscribe;
                    return result;
                }

                NotificationType notification = ((SendNotificationResponseMessageType)responseMessage).Notification;

               //string subscriptionId = notification.SubscriptionId;
              
                // If subscription Id is not the current subscription Id, unsubscribe
                //if (subscriptionId != _parent.SubscriptionId)
                //{
                //    unsubscribe = true;
                //}
                //else
                //{
                //    for (int c = 0; c < notification.Items.Length; c++)
                //    {
                //        // Get only new mail events
                //        if (notification.ItemsElementName[c].ToString() == "NewMailEvent")
                //        {
                //            BaseObjectChangedEventType bocet = (BaseObjectChangedEventType)notification.Items[c];

                //            //_parent.ShowMessage(((ItemIdType)bocet.Item).Id);
                //        }
                //    }
                //}           
            }

            if (unsubscribe)
            {
                result.SubscriptionStatus = SubscriptionStatusType.Unsubscribe;
            }
            else
            {
                result.SubscriptionStatus = SubscriptionStatusType.OK;
            }


            return result;
        }
    }
}

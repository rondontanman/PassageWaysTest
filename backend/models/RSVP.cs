using System;
using System.Collections.Generic;

namespace backend
{
   public class RSVP
   {
       private RSVP(string value) { Value = value; }

       public string Value { get; set; }
       public User User { get; set; }

       public static RSVP NoResponse                { get { return new RSVP("No Response"); } }
       public static RSVP Attending                 { get { return new RSVP("Attending"); } }
       public static RSVP RemotelyAttending         { get { return new RSVP("Remotely Attending"); } }
       public static RSVP TentativelyAttending      { get { return new RSVP("Tentatively Attending"); } }
       public static RSVP NotAttending              { get { return new RSVP("Not Attending"); } }
   }
}
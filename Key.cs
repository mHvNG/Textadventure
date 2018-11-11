using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using ZuulCS;

namespace ZuulCS {

    public class Key : Keys {

      public Key(string name, int weight, string description) : base(name, weight, description) {
        
      }

      public override void Use(Object o) {
        //Check if type is Room
        if (o.GetType() == typeof(Room)) {
          Room room = (Room) o;
          room.Unlock();
        } else {
          //If o is not a room
          System.Console.WriteLine("Can't use a key on this Object");
        }
      }

      public override void Use() {
			  System.Console.WriteLine("Key::use()");
		  }
    }
}

using System;
using System.Collections.Generic;

namespace ZuulCS {

  public class Keys : Item {

    public Keys(string name, int weight, string description) : base(name, weight, description) {

    }

    public virtual void Use(Object o) {
      System.Console.WriteLine("Item::use(Object o)");
    }

    public virtual void Use() {
      System.Console.WriteLine("Item::use");
    }
  }
}

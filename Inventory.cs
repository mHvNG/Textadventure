using System;
using System.Collections.Generic;

namespace ZuulCS {

  public class Inventory {

    public List<Item> items;
    internal List<Item> Items { get => items; }

    private int maxSpace;

    public Inventory(int max) {
      items = new List<Item>();
      maxSpace = max;
    }

    public bool AddItem(Item item) {

      int curItems = 0;

      for (int i = Items.Count; i >= 0; i--) {
        curItems++;
      }

      if (curItems < maxSpace) {
        Items.Add(item);
        return true;
      }

      return false;
    }

    public Item TakeItem(Inventory inv, int o) {
      if (inv.AddItem(items[o])) {
        Item item = items[o];
        items.Remove(items[o]);
        return item;
      }
      return null;
    }

    public Item DropItem(Inventory inv, int o) {

      if (inv.AddItem(items[o])) {
        Item item = items[o];
        items.Remove(items[o]);
        return item;
      }
      return null;
    }
  }
}

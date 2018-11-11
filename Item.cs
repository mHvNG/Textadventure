using System;
using System.Collections.Generic;
using System.Text;

namespace ZuulCS {

    public class Item {

        protected int weight;
        protected int damage;
        protected int heal;
        protected String name;
        protected String description;

        public Item(string name, int weight, string description) {
          this.name = name;
          this.weight = weight;
          this.description = description;
        }

        public string Description {
          get { return this.description; }
	        set { this.description = value; }
        }

        public string GetName {
          get { return this.name; }
          set { this.name = value; }
        }

        public string GetDescription {
          get { return this.description; }
          set { this.description = value; }
        }
    }
}

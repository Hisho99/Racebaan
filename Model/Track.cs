using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{

public class Track
    {

        public string Name { get; set; }
        public LinkedList<Section> Sections { get; set; }

        public Track(string name, SectionTypes[] sections)
        {
            Name = name;
            Sections = new LinkedList<Section>();
            foreach (SectionTypes s in sections)
            {
                Section section = new Section(s);
                Sections.AddLast(section);
            }
        }
    }
}

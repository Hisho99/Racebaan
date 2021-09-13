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
            Sections = SectionArrayConverter(sections);

        }

        //Converts SectionTypes array to a LinkedList<Section> to make a full circuit.
        public LinkedList<Section> SectionArrayConverter(SectionTypes[] sections)
        {
            LinkedList<Section> s = new LinkedList<Section>();
            foreach (SectionTypes sectiontype in sections)
            {
                Section sec = new Section(sectiontype);
                Sections.AddLast(sec);
            }
            return s;
        }

    }
}

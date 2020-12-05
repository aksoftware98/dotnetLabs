using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNetLabs.Shared
{
    public enum VideoPrivacy
    {
        Public = 1, // Available for everyne 
        Unlisted = 2, // Available only through URL
        Private = 3 // Cannot be seen 
    }

    public enum Category
    {
        Education = 1,
        Sport = 2, 
        Space = 3, 
        Entertainment = 4, 
        Music = 5
    }

}

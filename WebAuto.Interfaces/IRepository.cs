using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAuto.Core;

namespace WebAuto.Interfaces
{
    public interface IRepository
    {
        Dictionary<string, string> GetUIMap(string filename);
        DataBucket GetData(string filename);
        SequenceGroup GetCommandSequences(string filename, string[] sequences);
    	string[] GetSequencesFromMaster(string filename);
    	UITestSuite[] GetTestSuites(string filename, string filename2);
    }
}

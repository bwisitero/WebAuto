using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAuto.Core
{
    public class UIData : List<Dictionary<string, string>>
    {
        public string DataName { get; set; }
        public UIData(string dataName, UIDataRaw[] dataRaws)
        {
            DataName = dataName;
            bool firstRow = true;
            var d = new Dictionary<int, string>();
            foreach (var uiDataRaw in dataRaws)
            {

                if (firstRow)
                {
                    if (!string.IsNullOrEmpty(uiDataRaw.Data01)) d.Add(1, uiDataRaw.Data01);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data02)) d.Add(2, uiDataRaw.Data02);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data03)) d.Add(3, uiDataRaw.Data03);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data04)) d.Add(4, uiDataRaw.Data04);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data05)) d.Add(5, uiDataRaw.Data05);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data06)) d.Add(6, uiDataRaw.Data06);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data07)) d.Add(7, uiDataRaw.Data07);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data08)) d.Add(8, uiDataRaw.Data08);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data09)) d.Add(9, uiDataRaw.Data09);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data10)) d.Add(10, uiDataRaw.Data10);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data11)) d.Add(11, uiDataRaw.Data11);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data12)) d.Add(12, uiDataRaw.Data12);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data13)) d.Add(13, uiDataRaw.Data13);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data14)) d.Add(14, uiDataRaw.Data14);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data15)) d.Add(15, uiDataRaw.Data15);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data16)) d.Add(16, uiDataRaw.Data16);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data17)) d.Add(17, uiDataRaw.Data17);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data18)) d.Add(18, uiDataRaw.Data18);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data19)) d.Add(19, uiDataRaw.Data19);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data20)) d.Add(20, uiDataRaw.Data20);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data21)) d.Add(21, uiDataRaw.Data21);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data22)) d.Add(22, uiDataRaw.Data22);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data23)) d.Add(23, uiDataRaw.Data23);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data24)) d.Add(24, uiDataRaw.Data24);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data25)) d.Add(25, uiDataRaw.Data25);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data26)) d.Add(26, uiDataRaw.Data26);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data27)) d.Add(27, uiDataRaw.Data27);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data28)) d.Add(28, uiDataRaw.Data28);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data29)) d.Add(29, uiDataRaw.Data29);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data30)) d.Add(30, uiDataRaw.Data30);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data31)) d.Add(31, uiDataRaw.Data31);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data32)) d.Add(32, uiDataRaw.Data32);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data33)) d.Add(33, uiDataRaw.Data33);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data34)) d.Add(34, uiDataRaw.Data34);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data35)) d.Add(35, uiDataRaw.Data35);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data36)) d.Add(36, uiDataRaw.Data36);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data37)) d.Add(37, uiDataRaw.Data37);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data38)) d.Add(38, uiDataRaw.Data38);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data39)) d.Add(39, uiDataRaw.Data39);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data40)) d.Add(40, uiDataRaw.Data40);
                    firstRow = false;
                }
                else
                {
                    var data = new Dictionary<string, string>();
                    if (!string.IsNullOrEmpty(uiDataRaw.Data01)) data.Add(d[1], uiDataRaw.Data01);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data02)) data.Add(d[2], uiDataRaw.Data02);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data03)) data.Add(d[3], uiDataRaw.Data03);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data04)) data.Add(d[4], uiDataRaw.Data04);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data05)) data.Add(d[5], uiDataRaw.Data05);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data06)) data.Add(d[6], uiDataRaw.Data06);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data07)) data.Add(d[6], uiDataRaw.Data07);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data08)) data.Add(d[8], uiDataRaw.Data08);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data09)) data.Add(d[9], uiDataRaw.Data09);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data10)) data.Add(d[10], uiDataRaw.Data10);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data11)) data.Add(d[11], uiDataRaw.Data11);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data12)) data.Add(d[12], uiDataRaw.Data12);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data13)) data.Add(d[13], uiDataRaw.Data13);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data14)) data.Add(d[14], uiDataRaw.Data14);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data15)) data.Add(d[15], uiDataRaw.Data15);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data16)) data.Add(d[16], uiDataRaw.Data16);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data17)) data.Add(d[17], uiDataRaw.Data17);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data18)) data.Add(d[18], uiDataRaw.Data18);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data19)) data.Add(d[19], uiDataRaw.Data19);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data20)) data.Add(d[20], uiDataRaw.Data20);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data21)) data.Add(d[21], uiDataRaw.Data21);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data22)) data.Add(d[22], uiDataRaw.Data22);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data23)) data.Add(d[23], uiDataRaw.Data23);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data24)) data.Add(d[24], uiDataRaw.Data24);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data25)) data.Add(d[25], uiDataRaw.Data25);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data26)) data.Add(d[26], uiDataRaw.Data26);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data27)) data.Add(d[27], uiDataRaw.Data27);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data28)) data.Add(d[28], uiDataRaw.Data28);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data29)) data.Add(d[29], uiDataRaw.Data29);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data30)) data.Add(d[30], uiDataRaw.Data30);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data31)) data.Add(d[31], uiDataRaw.Data31);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data32)) data.Add(d[32], uiDataRaw.Data32);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data33)) data.Add(d[33], uiDataRaw.Data33);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data34)) data.Add(d[34], uiDataRaw.Data34);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data35)) data.Add(d[35], uiDataRaw.Data35);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data36)) data.Add(d[36], uiDataRaw.Data36);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data37)) data.Add(d[37], uiDataRaw.Data37);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data38)) data.Add(d[38], uiDataRaw.Data38);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data39)) data.Add(d[39], uiDataRaw.Data39);
                    if (!string.IsNullOrEmpty(uiDataRaw.Data40)) data.Add(d[40], uiDataRaw.Data40);
                    this.Add(data);
                }


            }
        }


    }
}

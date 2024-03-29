﻿using HchWebPhr.Utilities.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using static ShareDll.PHR;

namespace HchWebPhr.Service
{
    public class HchService
    {
        const string ApiToken = "B79CFD08-A462-47BA-95B3-5259A6632539";
#if DEBUG || STAGE
    #if DEBUG_API
            const string ApiBaseUrl = "http://localhost:56694/";
    #elif IN_HCH
            const string ApiBaseUrl = "http://192.168.10.169/WebApiTest";
    #elif DEBUG_PAGE_IN_PROD
            const string ApiBaseUrl = "http://192.168.10.169/WebApi";
    #else
            const string ApiBaseUrl = "http://web.hch.org.tw/WebApiTest";
    #endif
#else
        const string ApiBaseUrl = "http://192.168.10.169/WebApi";
#endif

        public IList<PT> GetPatientByEMailAndBirthDate(string EMail, DateTime BirthDate)
        {
            RsPT result = this.PostRequest<RsPT>("PHR_0001_001", new[] {
                new KeyValuePair<string, string>("email",EMail),
                new KeyValuePair<string, string>("birthday",BirthDate.toTaiwanDate(clearDelimiter: true))
            });
            if (result == null)
            {
                this.ErrorCode = "404";
                this.ErrorMessage = "沒有符合的病人資料！";
                return null;
            } else
            {
                return result.PT;
            }
        }

        public IList<PT> GetPatientByIdNoAndBirthDate(string IdNo, DateTime BirthDate)
        {
            RsPT result = this.PostRequest<RsPT>("PHR_0001_002", new[] {
                new KeyValuePair<string, string>("IdNo", IdNo),
                new KeyValuePair<string, string>("birthday",BirthDate.toTaiwanDate(clearDelimiter: true))
            });
            if (result == null)
            {
                this.ErrorCode = "404";
                this.ErrorMessage = "沒有符合的病人資料！";
                return null;
            }
            else
            {
                return result.PT;
            }
        }

        public IList<ImgList> GetAlbumListByChartNoAndDateRange(string ChartNo, DateTime StartDate, DateTime EndDate)
        {
            RsImgList result = this.PostRequest<RsImgList>("PHR_0002_001", new[] {
                new KeyValuePair<string, string>("ChartNo", ChartNo),
                new KeyValuePair<string, string>("StartDate", StartDate.toTaiwanDate(clearDelimiter: true)),
                new KeyValuePair<string, string>("EndDate", EndDate.toTaiwanDate(clearDelimiter: true))
            });
            if (result == null)
            {
                return null;
            } else
            {
                return result.ImgList;
            }
        }

        public IList<ImgDetail> GetPhotosByApplyNo(string ApplyNo)
        {
            RsImgDetail result = this.PostRequest<RsImgDetail>("PHR_0002_002", new[] {
                new KeyValuePair<string, string>("ApplyNo", ApplyNo)
            });
            if (result == null)
            {
                return null;
            } else
            {
                return result.ImgDetail;
            }
        }

        public IList<ImgList> GetAlbumByApplyNo(string ApplyNo)
        {
            RsImgList result = this.PostRequest<RsImgList>("PHR_0002_003", new[] {
                new KeyValuePair<string, string>("ApplyNo", ApplyNo)
            });
            if (result == null)
            {
                return null;
            } else
            {
                return result.ImgList;
            }
        }

        public IList<LabList> GetLabListByChartNoAndDateRange(string ChartNo, DateTime StartDate, DateTime EndDate)
        {
            RsLabList result = this.PostRequest<RsLabList>("PHR_0003_001", new[] {
                new KeyValuePair<string, string>("ChartNo", ChartNo),
                new KeyValuePair<string, string>("StartDate", StartDate.toTaiwanDate(clearDelimiter: true)),
                new KeyValuePair<string, string>("EndDate", EndDate.toTaiwanDate(clearDelimiter: true))
            });
            if (result == null)
            {
                return null;
            } else
            {
                return result.LabList;
            }
        }

        public IList<LabList> GetChildLabListByChartNoAndDateRange(string ChartNo, DateTime StartDate, DateTime EndDate)
        {
            RsLabList result = this.PostRequest<RsLabList>("PHR_0003_005", new[] {
                new KeyValuePair<string, string>("ChartNo", ChartNo),
                new KeyValuePair<string, string>("StartDate", StartDate.toTaiwanDate(clearDelimiter: true)),
                new KeyValuePair<string, string>("EndDate", EndDate.toTaiwanDate(clearDelimiter: true))
            });
            if (result == null)
            {
                return null;
            }
            else
            {
                return result.LabList;
            }
        }

        public IList<LabItem> GetNormalLabResultByLabNo(string LabNo)
        {
            RsLabItemX result = this.PostRequest<RsLabItemX>("PHR_0003_002", new[] {
                new KeyValuePair<string, string>("LabNo", LabNo)
            });
            if (result == null)
            {
                return null;
            } else
            {
                return result.LabItem;
            }
        }

        public bool GetDiagnosisLabResultByLabNo(string LabNo, out string DiagnosisResult, out string Recommendation)
        {
            DiagnosisResult = "";
            Recommendation = "";
            RsLabItemG result = this.PostRequest<RsLabItemG>("PHR_0003_003", new[]
            {
                new KeyValuePair<string, string>("LabNo", LabNo)
            });
            if (result == null)
            {
                return false;
            } else
            {
                DiagnosisResult = result.Diagnosis;
                Recommendation = result.Comment;
                return true;
            }
        }

        public bool GetPdfLabResultByLabNo(string LabNo, out string PdfResultFilePath)
        {
            PdfResultFilePath = "";
            RsLabItemN result = this.PostRequest<RsLabItemN>("PHR_0003_004", new[]
            {
                new KeyValuePair<string, string>("LabNo", LabNo)
            });
            if (result == null)
            {
                return false;
            } else
            {
                PdfResultFilePath = result.FileAddress;
                return true;
            }
        }

        public bool GetVideoLabResultByLabNo(string LabNo, out string VideoResultFilePath, out DateTime ReportDateTime)
        {
            VideoResultFilePath = "";
            ReportDateTime = DateTime.MinValue;
            RsLabItemS result = this.PostRequest<RsLabItemS>("PHR_0003_006", new[] {
                new KeyValuePair<string, string>("LabNo", LabNo)
            });
            if (result == null)
            {
                return false;
            } else
            {
                VideoResultFilePath = result.FileAddress;
                ReportDateTime = (result.IsExist ? result.ReportDateTime : DateTime.MaxValue);
                return true;
            }
        }

        public IList<ClinicData> GetMedListByChartNoAndDateRange(string ChartNo, DateTime StartDate, DateTime EndDate)
        {
            RsMedList result = this.PostRequest<RsMedList>("PHR_0004_001", new[] {
                new KeyValuePair<string, string>("ChartNo", ChartNo),
                new KeyValuePair<string, string>("StartDate", StartDate.toTaiwanDate(clearDelimiter: true)),
                new KeyValuePair<string, string>("EndDate", EndDate.toTaiwanDate(clearDelimiter: true))
            });
            if (result == null)
            {
                return null;
            }
            else
            {
                return result.MedList;
            }
        }

        public IList<MedDetail> GetMedDetailByChartNoDivNoAndDate(string ChartNo, DateTime ClinicDate, string DivNo)
        {
            RsMedDetail result = this.PostRequest<RsMedDetail>("PHR_0004_002", new[] {
                new KeyValuePair<string, string>("ChartNo", ChartNo),
                new KeyValuePair<string, string>("ClinicDate", ClinicDate.toTaiwanDate(clearDelimiter: true)),
                new KeyValuePair<string, string>("DivNo", DivNo)
            });
            if (result == null)
            {
                return null;
            } else
            {
                return result.MedDetail;
            }
        }

        public IList<PregnancyCheckDetail> GetPregnancyListByChartNoAndDateRange(string ChartNo, DateTime StartDate, DateTime EndDate)
        {
            RsCheckDetail result = this.PostRequest<RsCheckDetail>("PHR_0005_001", new[] {
                new KeyValuePair<string, string>("ChartNo", ChartNo),
                new KeyValuePair<string, string>("StartDate", StartDate.toTaiwanDate(clearDelimiter: true)),
                new KeyValuePair<string, string>("EndDate", EndDate.toTaiwanDate(clearDelimiter: true))
            });
            if (result == null)
            {
                return null;
            } else
            {
                return result.CheckDetail;
            }
        }

        public IList<ChildData> GetChildrenListByMomChartNo(string ChartNo)
        {
            RsChildData result = this.PostRequest<RsChildData>("PHR_0006_001", new[] {
                new KeyValuePair<string, string>("ChartNo", ChartNo)
            });
            if (result == null)
            {
                return null;
            } else
            {
                return result.ChildData;
            }
        }

        public IList<Vaccine> GetChildrenVaccineDetailListByChildChartNoAndDateRange(string ChartNo, DateTime StartDate, DateTime EndDate)
        {
            RsVaccine result = this.PostRequest<RsVaccine>("PHR_0006_002", new[] {
                new KeyValuePair<string, string>("ChartNo", ChartNo),
                new KeyValuePair<string, string>("StartDate", StartDate.toTaiwanDate(clearDelimiter: true)),
                new KeyValuePair<string, string>("EndDate", EndDate.toTaiwanDate(clearDelimiter: true))
            });
            if (result == null)
            {
                return null;
            } else
            {
                return result.VaccineList;
            }
        }

        private T PostRequest<T>(string Fid, params KeyValuePair<string,string>[] Paras)
        {
            T resultObj;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiBaseUrl);
                IList<KeyValuePair<string, string>> FormContentList = new List<KeyValuePair<string, string>>();
                FormContentList.Add(new KeyValuePair<string, string>("Token", ApiToken));
                FormContentList.Add(new KeyValuePair<string, string>("FID", Fid));
                foreach (var KVP in Paras)
                {
                    FormContentList.Add(KVP);
                }
                var content = new FormUrlEncodedContent(FormContentList);
                var result = client.PostAsync(client.BaseAddress.ToString() + this.GetRequestUrl(Fid), content).Result;
                if (result.IsSuccessStatusCode)
                {
                    var resultString = result.Content.ReadAsStringAsync().Result;
                    resultObj = JsonConvert.DeserializeObject<T>(resultString);
                }
                else
                {
                    this.ErrorCode = result.StatusCode.ToString();
                    this.ErrorMessage = result.ReasonPhrase;
                    resultObj = default(T);
                }
            }
            return resultObj;
        }

        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        private string GetRequestUrl(string Fid)
        {
            switch (Fid)
            {
                case "PHR_0001_001":
                case "PHR_0001_002":
                    return "/PT";
                case "PHR_0002_001":
                case "PHR_0002_002":
                case "PHR_0002_003":
                    return "/PhrImg";
                case "PHR_0003_001":
                case "PHR_0003_002":
                case "PHR_0003_003":
                case "PHR_0003_004":
                case "PHR_0003_005":
                case "PHR_0003_006":
                    return "/PhrLab";
                case "PHR_0004_001":
                case "PHR_0004_002":
                    return "/PhrMed";
                case "PHR_0005_001":
                    return "/PhrPregnancy";
                case "PHR_0006_001":
                case "PHR_0006_002":
                    return "/PhrVaccine";
                default:
                    return "";
            }
        }
    }
}

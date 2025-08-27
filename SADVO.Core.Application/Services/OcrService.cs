using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADVO.Core.Application.Services
{
    using System;
    using Tesseract;

    namespace SADVO.Core.Application.Services
    {
        public class OcrService
        {
            private readonly string _tessdataPath;

            public OcrService(string tessdataPath)
            {
                _tessdataPath = tessdataPath;
            }

            public string LeerTextoDesdeImagen(string rutaImagen)
            {
                try
                {
                    using var engine = new TesseractEngine(_tessdataPath, "spa", EngineMode.Default);
                    using var img = Pix.LoadFromFile(rutaImagen);
                    using var page = engine.Process(img);
                    return page.GetText();
                }
                catch (Exception ex)
                {
                    
                    return $"Error OCR: {ex.Message}";
                }
            }
        }
    }

}

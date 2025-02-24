using ACME_ABASTECIMIENTO.Models;
using ACME_ABASTECIMIENTO.Services.Interfaces;
using System.Text;
using System.Xml.Linq;

namespace ACME_ABASTECIMIENTO.Services
{
    public class AbastecimientoService : IAbastecimientoService
    {
        public AbastecimientoService()
        {
        }

        public async Task<AbastecimientoRespuesta> EnviarInformacion(AbastecimientoParametro abastecimientoParametro)
        {
            string xml = ConvertJsonToXml(abastecimientoParametro);
            string response = await CallSoapServiceAsync(xml);
            return ConvertXmlToJson(response);
        }

        public async Task<string> CallSoapServiceAsync(string soapRequestXml)
        {
            string soapUrl = "https://run.mocky.io/v3/19217075-6d4e-4818-98bc-416d1feb7b84";
            string soapAction = "http://WSDLs/EnvioPedidos/EnvioPedidosAcme/EnvioPedidoAcme";
            StringContent soapRequestBody = new StringContent(soapRequestXml, Encoding.UTF8, "text/xml");

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("SOAPAction", soapAction);
                var response = await httpClient.PostAsync(soapUrl, soapRequestBody);
                string responseContent = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(responseContent))
                    responseContent = @"<soapenv:Envelope xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:env='http://WSDLs/EnvioPedidos/EnvioPedidosAcme'>
                                            <soapenv:Header/>
                                            <soapenv:Body>
                                                <env:EnvioPedidoAcmeResponse>
                                                    <EnvioPedidoResponse>
                                                        <Codigo>80375472</Codigo>
                                                        <Mensaje>Entregado exitosamente al cliente</Mensaje>
                                                    </EnvioPedidoResponse>
                                                </env:EnvioPedidoAcmeResponse>
                                            </soapenv:Body>
                                        </soapenv:Envelope>";

                return responseContent;
            }
        }

        public string ConvertJsonToXml(AbastecimientoParametro jsonRequest)
        {
            XNamespace soapenv = "http://schemas.xmlsoap.org/soap/envelope/";
            XNamespace env = "http://WSDLs/EnvioPedidos/EnvioPedidosAcme";

            var xml = new XElement(soapenv + "Envelope",
                new XAttribute(XNamespace.Xmlns + "soapenv", soapenv),
                new XAttribute(XNamespace.Xmlns + "env", env),
                new XElement(soapenv + "Header"),
                new XElement(soapenv + "Body",
                    new XElement(env + "EnvioPedidoAcme",
                        new XElement("EnvioPedidoRequest",
                            new XElement("pedido", jsonRequest.NumPedido),
                            new XElement("Cantidad", jsonRequest.CantidadPedido),
                            new XElement("EAN", jsonRequest.CodigoEAN),
                            new XElement("Producto", jsonRequest.NombreProducto),
                            new XElement("Cedula", jsonRequest.NumDocumento),
                            new XElement("Direccion", jsonRequest.Direccion)
                        )
                    )
                )
            );

            return xml.ToString();
        }

        public AbastecimientoRespuesta ConvertXmlToJson(string xml)
        {
            var xmlParsed = XElement.Parse(xml);
            return new AbastecimientoRespuesta
            {
                CodigoEnvio = xmlParsed.Descendants("Codigo").FirstOrDefault()?.Value,
                Estado = xmlParsed.Descendants("Mensaje").FirstOrDefault()?.Value
            };
        }
    }
}

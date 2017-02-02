using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using Nancy;

namespace LanchoneteMongoDB
{
    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get["/"] = x => { return "Hello World"; };

            Get["/{categoria}/{sabor}"] = parameters =>
            {
                InserirPedido(parameters.categoria, parameters.sabor);

                return "Foi adicionado uma " + parameters.categoria + " com o sabor " + parameters.sabor;
            };

            Get["/{categoria}"] = parameters =>
            {
                StringBuilder resposta = new StringBuilder();

                var collection = MongoDB.Instance().GetCollection<BsonDocument>("pedidos");

                FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("pedido.categoria", parameters.categoria.ToString());
                var pedidosFiltrados = collection.Find(filter).ToList();

                foreach (BsonDocument document in pedidosFiltrados)
                {
                    var categoria = document.GetValue("pedido").ToBsonDocument().GetValue("categoria").AsString;
                    var sabor = document.GetValue("pedido").ToBsonDocument().GetValue("sabor").AsString;

                    resposta.Append(string.Format("Um pedido de {0} com o sabor {1} está gravado\n", categoria, sabor));
                }

                if (string.IsNullOrEmpty(resposta.ToString()))
                    return "Nenhum pedido de " + parameters.categoria;

                return resposta.ToString();
            };
        }

        private void InserirPedido(string categoria, string sabor)
        {
            var document = new BsonDocument
            {
                {
                    "pedido", new BsonDocument
                    {
                        {"categoria", categoria},
                        {"sabor", sabor}
                    }
                }
            };

            var collection = MongoDB.Instance().GetCollection<BsonDocument>("pedidos");
            collection.InsertOneAsync(document);
        }
    }
}

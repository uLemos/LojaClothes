using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;


namespace crudProdutos
{
    public static class AppUtils
    {
        public static async Task ProcessarArquivoDeImagem(int idProduto, IFormFile imagemProduto, IWebHostEnvironment whe)
        {
            //Copia a imagem para um stream em memória
            var memoryStream = new MemoryStream();
            await imagemProduto.CopyToAsync(memoryStream);
            
            //carrega o stream em memória para o objeto de processamento de imagem
            memoryStream.Position = 0;
            var img = await Image.LoadAsync(memoryStream);
            JpegEncoder jpegEnc = new JpegEncoder();
            jpegEnc.Quality = 100;
            img.SaveAsJpeg(memoryStream, jpegEnc);
            memoryStream.Position = 0;
            img = await Image.LoadAsync(memoryStream);
            memoryStream.Close();
            memoryStream.Dispose();

            //Cria um retângulo de recorte para deixar a imagem quadrada
            var tamanho = img.Size();
            Rectangle retanguloCorte;
            if (tamanho.Width > tamanho.Height)
            {
                float x = (tamanho.Width - tamanho.Height) / 2.0F;
                retanguloCorte = new Rectangle((int)x, 0, tamanho.Height, tamanho.Height);
            }
            else
            {
                float y = (tamanho.Height - tamanho.Width) / 2.0F;
                retanguloCorte = new Rectangle(0, (int)y, tamanho.Width, tamanho.Width);
            }
            //recorta a imagem usando o retângulo computado
            img.Mutate(i => i.Crop(retanguloCorte));
            //Monta o caminho da imagem "(~/img/produto/0000.png)"
            var caminhoArquivoImagem = Path.Combine(whe.WebRootPath, "img\\produto", idProduto.ToString("D6") + ".png");
            //Cria um arquivo de imagem sobreescrevendo o existente, caso exista
            await img.SaveAsync(caminhoArquivoImagem);
        }
    }
}

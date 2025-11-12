using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChamadosC_
{
    public static class GeradorPDF
    {
        public static void GerarChamado()
        {
            string newFile = @"pdf/chamado.pdf";
            Directory.CreateDirectory("pdf");

            Document doc = new Document();
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(newFile, FileMode.Create));
            doc.Open();

            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

            Font tituloFont = new Font(bf, 26f, Font.BOLD, BaseColor.BLACK);
            Font infoLabelFont = new Font(bf, 10f, Font.BOLD, BaseColor.WHITE);
            Font infoValueFont = new Font(bf, 10f, Font.NORMAL, BaseColor.BLACK);
            Font headerFont = new Font(bf, 13f, Font.BOLD, BaseColor.WHITE);
            Font subHeaderFont = new Font(bf, 11f, Font.BOLD, BaseColor.WHITE);
            Font contentFont = new Font(bf, 10f, Font.NORMAL, BaseColor.BLACK);
            Font rodapeFont = new Font(bf, 9f, Font.BOLDITALIC, BaseColor.BLACK);
            Font assinaturaFont = new Font(bf, 9f, Font.BOLDITALIC, BaseColor.BLACK);


            // Logo
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance("img/logo.png");
            logo.ScalePercent(30f);

            doc.Add(logo);
            




            Paragraph space = new Paragraph("\n");

            // Título principal
            Paragraph titulo = new Paragraph(new Phrase("Registro do Chamado - N° 123456", tituloFont));
            titulo.Alignment = Element.ALIGN_CENTER;
            doc.Add(titulo);
            doc.Add(space);

            // Informações do chamado
            PdfPTable info = new PdfPTable(2);
            info.WidthPercentage = 100;
            info.SetWidths(new float[] { 19f, 55f });

            string[,] infodados = {
                {"Registro da Solicitação:", "16/09/2025 10:29:10"},
                {"Número do Chamado:", "123456"},
                {"Identificador da Máquina:", "D4F9A8C2-7B3E-4E91-BC1A-9F6E2A3C1D7F"},
                {"Solicitante:", "Gustavo Ribeiro - Unidade Braz Cubas"},
                {"Problema Relatado:", "Computador Lento"},
                {"Técnico Responsável:", "Gustavo"},
                {"Momento do Contato:", "16/09/2025 10:50:34"}
            };

            for (int i = 0; i < infodados.GetLength(0); i++)
            {
                PdfPCell label = new PdfPCell(new Phrase(infodados[i, 0], infoLabelFont));
                label.BackgroundColor = new BaseColor(49, 102, 173);
                label.Border = Rectangle.BOTTOM_BORDER;
                label.BorderColor = BaseColor.WHITE;
                label.HorizontalAlignment = Element.ALIGN_LEFT;
                label.Padding = 5;

                PdfPCell value = new PdfPCell(new Phrase(infodados[i, 1], infoValueFont));
                value.BackgroundColor = (i % 2 == 0) ? new BaseColor(240, 240, 240) : BaseColor.WHITE;
                value.Border = Rectangle.NO_BORDER;
                value.HorizontalAlignment = Element.ALIGN_LEFT;
                value.Padding = 5;

                info.AddCell(label);
                info.AddCell(value);
            }

            doc.Add(info);
            doc.Add(space);

            // Materiais utilizados
            Paragraph titulo2 = new Paragraph(new Phrase("- Lista de Materiais Usados no Chamado -", new Font(bf, 18f, Font.BOLD, BaseColor.BLACK)));
            titulo2.Alignment = Element.ALIGN_CENTER;
            doc.Add(titulo2);
            doc.Add(space);

            PdfPTable materiaisHeader = new PdfPTable(1);
            materiaisHeader.WidthPercentage = 100;
            PdfPCell headerCell = new PdfPCell(new Phrase("Materiais Utilizados no Chamado:", headerFont));
            headerCell.BackgroundColor = new BaseColor(49, 102, 173);
            headerCell.Border = Rectangle.BOTTOM_BORDER;
            headerCell.BorderColor = BaseColor.WHITE;
            headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
            headerCell.Padding = 5;
            materiaisHeader.AddCell(headerCell);
            doc.Add(materiaisHeader);

            PdfPTable materiaisTabela = new PdfPTable(2);
            materiaisTabela.WidthPercentage = 100;
            materiaisTabela.SetWidths(new float[] { 68f, 20f });

            string[,] materiais = {
                {"Descrição do Material Utilizado", "Quantidade"},
                {"SSD 480GB Kingston", "1"},
                {"Memória RAM DDR4 8GB", "1"}
            };

            for (int i = 0; i < materiais.GetLength(0); i++)
            {
                Font fonte = (i == 0) ? subHeaderFont : contentFont;
                BaseColor bg = (i == 0) ? new BaseColor(49, 102, 173) : (i % 2 == 0 ? new BaseColor(240, 240, 240) : BaseColor.WHITE);

                PdfPCell desc = new PdfPCell(new Phrase(materiais[i, 0], fonte));
                desc.BackgroundColor = bg;
                desc.Border = Rectangle.NO_BORDER;
                desc.HorizontalAlignment = Element.ALIGN_LEFT;
                desc.Padding = 10;

                PdfPCell qtd = new PdfPCell(new Phrase(materiais[i, 1], fonte));
                qtd.BackgroundColor = bg;
                qtd.Border = Rectangle.LEFT_BORDER;
                qtd.BorderColor = new BaseColor(204, 204, 204);
                qtd.HorizontalAlignment = Element.ALIGN_CENTER;
                qtd.Padding = 10;

                materiaisTabela.AddCell(desc);
                materiaisTabela.AddCell(qtd);
            }

            doc.Add(materiaisTabela);
            doc.Add(space);

            // Processo da solução
            Paragraph titulo3 = new Paragraph(new Phrase("- CheckList da Solução do Problema -", new Font(bf, 16f, Font.BOLD, BaseColor.BLACK)));
            titulo3.Alignment = Element.ALIGN_CENTER;
            doc.Add(titulo3);
            doc.Add(space);

            PdfPTable processoHeader = new PdfPTable(1);
            processoHeader.WidthPercentage = 100;
            PdfPCell processoTitulo = new PdfPCell(new Phrase("Métodos Realizados", headerFont));
            processoTitulo.BackgroundColor = new BaseColor(49, 102, 173);
            processoTitulo.Border = Rectangle.BOTTOM_BORDER;
            processoTitulo.BorderColor = BaseColor.WHITE;
            processoTitulo.HorizontalAlignment = Element.ALIGN_CENTER;
            processoTitulo.Padding = 5;
            processoHeader.AddCell(processoTitulo);
            doc.Add(processoHeader);

            PdfPTable processoTabela = new PdfPTable(1);
            processoTabela.WidthPercentage = 100;

            string processoTexto =
                "Realizar backup completo dos dados do usuário\n\n" +
                "Retirar o HD ou SSD Antigo do Usuário e colocar a peça nova\n\n" +
                "Verificar a compatibilidade da Memória RAM se ela é DDR3 ou DDR4 e adicionar a memória\n\n" +
                "Reinstalar as aplicações do usuário";

            PdfPCell processoCelula = new PdfPCell(new Phrase(processoTexto, contentFont));
            processoCelula.BackgroundColor = new BaseColor(240, 240, 240);
            processoCelula.Border = Rectangle.BOTTOM_BORDER;
            processoCelula.BorderColor = BaseColor.WHITE;
            processoCelula.HorizontalAlignment = Element.ALIGN_LEFT;
            processoCelula.Padding = 7;
            processoTabela.AddCell(processoCelula);
            doc.Add(processoTabela);
            doc.Add(space);

            // Rodapé
            Paragraph rodape = new Paragraph("Emitido em: 19 de setembro de 2025\nDocumento gerado automaticamente. Válido para fins internos.", rodapeFont);
            rodape.Alignment = Element.ALIGN_RIGHT;
            doc.Add(rodape);
            doc.Add(space);

            // Assinatura
            PdfPTable assinatura = new PdfPTable(1);
            assinatura.WidthPercentage = 100;
            PdfPCell assinaturaCelula = new PdfPCell(new Phrase("\n\n\n\n       ________________________________________________________.\n\n              Assinatura do técnico.", assinaturaFont));
            assinaturaCelula.Border = Rectangle.NO_BORDER;
            assinaturaCelula.HorizontalAlignment = Element.ALIGN_CENTER;

            assinatura.AddCell(assinaturaCelula);
            doc.Add(assinatura);

            doc.Close();
        }
        public static void pedidocompra()
        {
            string newFile = @"pdf\pedidocompra.pdf";

            Document doc = new Document();
            PdfWriter write = PdfWriter.GetInstance(doc, new FileStream(newFile, FileMode.Create));

            doc.Open();

            PdfContentByte cb = write.DirectContentUnder;

            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetColorFill(BaseColor.BLACK);
            cb.SetFontAndSize(bf, 20);

            #region Logo  Waldesa

            var imagem = iTextSharp.text.Image.GetInstance(@"img/logo.png");
            imagem.ScalePercent(25f);
            imagem.SetAbsolutePosition(doc.LeftMargin, doc.PageSize.Height - 60);

            doc.Add(imagem);

            #endregion

            #region Espaço entre o titulo e a logo

            Paragraph espaco1 = new Paragraph("\n");
            doc.Add(espaco1);

            #endregion



            #region titulo e espaço do titulo

            Paragraph titulo = new Paragraph(new Phrase(" Pedido de Compra - Pedido Nº 123456", new Font(bf, 26f, Font.BOLD, BaseColor.BLACK)));
            titulo.Alignment = Element.ALIGN_CENTER;

            Paragraph espaço = new Paragraph("\n\n");

            doc.Add(titulo);
            doc.Add(espaço);

            #endregion

            #region informações do pedido

            PdfPTable info = new PdfPTable(2);
            info.WidthPercentage = 100;
            info.SetWidths(new float[] { 18f, 68f });

            Font labelFont = new Font(bf, 10f, Font.BOLD, BaseColor.WHITE);
            Font valueFont = new Font(bf, 10f, Font.NORMAL, BaseColor.BLACK);

            string[,] dados = {
{ "Número do Pedido:", "#123456" },
{ "Solicitante:", "Gustavo Silva" },
{ "Unidade:", "Santa Efigênia" },
{ "Departamento:", "Compras" },
{ "Uso do Material:", "Produção de evento interno" },
{ "Autorizado por:", "João Pereira" }
        };

            for (int i = 0; i < dados.GetLength(0); i++)
            {
                BaseColor bgcolor = (i % 2 == 0) ? new BaseColor(240, 240, 240) : BaseColor.WHITE;

                PdfPCell label = new PdfPCell(new Phrase(dados[i, 0], labelFont));
                label.BackgroundColor = new BaseColor(49, 102, 173);
                label.Border = Rectangle.BOTTOM_BORDER;
                label.HorizontalAlignment = Element.ALIGN_CENTER;
                label.BorderColor = BaseColor.WHITE;
                label.Padding = 5;

                PdfPCell value = new PdfPCell(new Phrase(dados[i, 1], valueFont));
                value.BackgroundColor = bgcolor;
                value.Border = Rectangle.NO_BORDER;
                value.Padding = 5;
                info.AddCell(label);
                info.AddCell(value);
            }

            doc.Add(info);
            doc.Add(new Paragraph("\n"));

            #endregion

            #region tabela de produtos

            PdfPTable tabela = new PdfPTable(6);
            tabela.WidthPercentage = 100;
            tabela.SetWidths(new float[] { 10f, 30f, 10f, 15f, 15f, 20f });
            tabela.HeaderRows = 1;

            Font headerFont = new Font(bf, 9f, Font.BOLD, BaseColor.WHITE);
            Font cellFont = new Font(bf, 9f, Font.NORMAL, BaseColor.BLACK);

            #region Header
            string[] headers = {
            "Código", "Descrição do Produto", "Qtde",
            "Valor Unitário Aproximado", "Valor Total", "Recomendação de Fornecedor"
        };

            foreach (var h in headers)
            {
                PdfPCell cell = new PdfPCell(new Phrase(h, headerFont));
                cell.BackgroundColor = new BaseColor(49, 102, 173);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                cell.Padding = 6;
                tabela.AddCell(cell);
                #endregion


            }

            #region Gerador de Produtos
            List<string[]> produtosList = new List<string[]>();

            for (int i = 1; i <= 120; i++)
            {
                string codigo = i.ToString("D3");
                string descricao = $"Produto exemplo {i}";
                string quantidade = (i % 10 + 1).ToString();
                double valorUnit = i * 2.5;
                double total = valorUnit * int.Parse(quantidade);
                string valorUnitario = $"R$ {valorUnit:0.00}";
                string valorTotal = $"R$ {total:0.00}";
                string fornecedor = $"Fornecedor {((i % 5) + 1)}";

                produtosList.Add(new string[] { codigo, descricao, quantidade, valorUnitario, valorTotal, fornecedor });
            }
            #endregion


            for (int i = 0; i < produtosList.Count; i++)
            {
                var produto = produtosList[i];
                BaseColor bgColor = (i % 2 == 0) ? new BaseColor(230, 230, 230) : BaseColor.WHITE;

                for (int j = 0; j < produto.Length; j++)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(produto[j], cellFont));
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.Padding = 5;
                    cell.BackgroundColor = bgColor;
                    cell.Border = Rectangle.NO_BORDER;
                    tabela.AddCell(cell);
                }
            }

            doc.Add(tabela);
            doc.Add(new Paragraph("\n"));

            #endregion

            #region rodapé

            Paragraph rodape = new Paragraph(" Emitido em: 10 de setembro de 2025\n Documento gerado automaticamente. Válido para fins internos.",
                new Font(bf, 9f, Font.ITALIC, BaseColor.BLACK));
            rodape.Alignment = Element.ALIGN_RIGHT;

            doc.Add(rodape);

            #endregion



            doc.Close();




        }





    }
}

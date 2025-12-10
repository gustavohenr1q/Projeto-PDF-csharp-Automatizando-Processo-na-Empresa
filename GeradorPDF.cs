using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
        public static void GerarRelatorio()
        {
            #region Instâncias e Fontes

            string newFile = @"pdf/relatorio.pdf";
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

            #endregion

            #region Titulo

            Paragraph space = new Paragraph("\n");

            Paragraph titulo = new Paragraph(new Phrase("WALDESA MOTOMERCANTIL LTDA", new Font(bf, 20f, Font.BOLD, BaseColor.BLACK)));
            titulo.Alignment = Element.ALIGN_CENTER;
            doc.Add(titulo);
            doc.Add(space);

            #endregion

            #region Primeira Tabela Sobre o Projeto

            PdfPTable info = new PdfPTable(2);
            info.WidthPercentage = 100;
            info.SetWidths(new float[] { 24f, 70f });

            string[,] projetoInfo = {
    {"TÍTULO DO PROJETO", "Expansão de Rede e Servidores – Unidade Braz Cubas"},
    {"ENVIADO POR", "Gustavo Ribeiro"},
    {"TIPO DE PROJETO", "Infraestrutura de TI"},
    {"PREVISÃO DE INÍCIO", "25/09/2025"},
    {"ORÇAMENTO TOTAL ESTIMADO", "R$ 12.500,00"},
    {"LOCAL DO PROJETO", "Unidade Braz Cubas"},
    {"DATA PREVISTA DE ENTREGA", "02/10/2025"}
};

            for (int i = 0; i < projetoInfo.GetLength(0); i++)
            {
                PdfPCell label = new PdfPCell(new Phrase(projetoInfo[i, 0], new Font(bf, 9f, Font.BOLD, BaseColor.WHITE)));
                label.BackgroundColor = new BaseColor(49, 102, 173);
                label.Border = Rectangle.BOTTOM_BORDER;
                label.BorderColor = BaseColor.WHITE;
                label.HorizontalAlignment = Element.ALIGN_RIGHT;
                label.Padding = 7;

                PdfPCell value = new PdfPCell(new Phrase(projetoInfo[i, 1], infoValueFont));
                value.BackgroundColor = (i % 2 == 0) ? new BaseColor(240, 240, 240) : BaseColor.WHITE;
                value.Border = Rectangle.NO_BORDER;
                value.Padding = 7;

                info.AddCell(label);
                info.AddCell(value);
            }

            doc.Add(info);
            doc.Add(space);

            #endregion

            #region Tabela de Materiais Utilizados

            PdfPTable materiaisHeader = new PdfPTable(1);
            materiaisHeader.WidthPercentage = 100;
            PdfPCell headerCell = new PdfPCell(new Phrase("MATERIAIS UTILIZADOS NO PROJETO", new Font(bf, 10f, Font.BOLD, BaseColor.WHITE)));
            headerCell.BackgroundColor = new BaseColor(7, 47, 99);
            headerCell.Border = Rectangle.BOTTOM_BORDER;
            headerCell.BorderColor = BaseColor.WHITE;
            headerCell.HorizontalAlignment = Element.ALIGN_LEFT;
            headerCell.Padding = 8;
            materiaisHeader.AddCell(headerCell);

            doc.Add(materiaisHeader);

            PdfPTable materiaisTabela = new PdfPTable(3);
            materiaisTabela.WidthPercentage = 100;
            materiaisTabela.SetWidths(new float[] { 40f, 40f, 40f });

            string[,] materiais = {
    {"MATERIAL", "QUANTIDADE", "VALOR"},
    {"Servidor Dell PowerEdge T40", "1", "R$ 6.500,00"},
    {"Switch Gerenciável 24 portas", "2", "R$ 3.000,00"},
    {"Cabeamento Estruturado Cat6 (100m)", "1", "R$ 1.000,00"},
    {"Nobreak APC 1500VA", "2", "R$ 1.500,00"},
    {"Serviço Técnico de Instalação e Configuração\r\n", "1", "R$ 500,00"}
};

            for (int i = 0; i < materiais.GetLength(0); i++)
            {
                Font headfonte = (i == 0) ? new Font(bf, 10f, Font.BOLD, BaseColor.WHITE) : infoLabelFont;
                BaseColor bg = (i == 0) ? new BaseColor(49, 102, 173) : (i % 2 == 0 ? new BaseColor(240, 240, 240) : BaseColor.WHITE);

                for (int j = 0; j < 3; j++)
                {

                    Font celule = (i == 0) ? new Font(bf, 8f, Font.BOLD, BaseColor.WHITE) : contentFont;

                    PdfPCell cell = new PdfPCell(new Phrase(materiais[i, j], celule));
                    cell.BackgroundColor = bg;
                    cell.BorderColor = BaseColor.WHITE;
                    cell.Border = Rectangle.RIGHT_BORDER;
                    cell.Padding = 7;
                    cell.HorizontalAlignment = (j == 2) ? Element.ALIGN_CENTER : Element.ALIGN_CENTER;
                    materiaisTabela.AddCell(cell);
                }
            }

            doc.Add(materiaisTabela);
            doc.Add(space);

            #endregion

            #region Visão Geral e Resumo do Projeto

            PdfPTable visaogeral = new PdfPTable(1);
            visaogeral.WidthPercentage = 100;
            PdfPCell visaog = new PdfPCell(new Phrase("VISÃO GERAL DO PROJETO", new Font(bf, 10f, Font.BOLD, BaseColor.WHITE)));
            visaog.BackgroundColor = new BaseColor(7, 47, 99);
            visaog.Border = Rectangle.BOTTOM_BORDER;
            visaog.BorderColor = BaseColor.WHITE;
            visaog.HorizontalAlignment = Element.ALIGN_LEFT;
            visaog.Padding = 8;
            visaogeral.AddCell(visaog);

            doc.Add(visaogeral);

            PdfPTable resumo = new PdfPTable(1);
            resumo.WidthPercentage = 100;
            PdfPCell resumocell = new PdfPCell(new Phrase("RESUMO", new Font(bf, 10f, Font.BOLD, BaseColor.WHITE)));
            resumocell.BackgroundColor = new BaseColor(49, 102, 173);
            resumocell.Border = Rectangle.BOTTOM_BORDER;
            resumocell.BorderColor = BaseColor.WHITE;
            resumocell.HorizontalAlignment = Element.ALIGN_LEFT;
            resumocell.Padding = 5;
            resumo.AddCell(resumocell);

            doc.Add(resumo);

            PdfPTable resumostring = new PdfPTable(1);
            resumostring.WidthPercentage = 100;

            string resumotext =
                "Realizar levantamento da infraestrutura atual da unidade.\n\n" +
                "Instalar novos servidores para centralização de dados e aplicações.\n\n" +
                "Implementar switches gerenciáveis para otimizar a rede interna.\n\n" +
                "Executar cabeamento estruturado para garantir maior estabilidade e velocidade.\n\n"+
                "Configurar nobreaks para proteção contra quedas de energia.\n\n" +
                "Testar conectividade e desempenho da rede após a instalação..\n\n";

            PdfPCell resumotextcell = new PdfPCell(new Phrase(resumotext, contentFont));
            resumotextcell.BackgroundColor = new BaseColor(240, 240, 240);
            resumotextcell.Border = Rectangle.BOTTOM_BORDER;
            resumotextcell.BorderColor = BaseColor.WHITE;
            resumotextcell.HorizontalAlignment = Element.ALIGN_LEFT;
            resumotextcell.Padding = 7;
            resumostring.AddCell(resumotextcell);

            doc.Add(resumostring);

            #endregion

            #region Adendos do Projeto

            PdfPTable adendos = new PdfPTable(1);
            adendos.WidthPercentage = 100;
            PdfPCell adendoscell = new PdfPCell(new Phrase("ADENDOS", new Font(bf, 10f, Font.BOLD, BaseColor.WHITE)));
            adendoscell.BackgroundColor = new BaseColor(49, 102, 173);
            adendoscell.Border = Rectangle.BOTTOM_BORDER;
            adendoscell.BorderColor = BaseColor.WHITE;
            adendoscell.HorizontalAlignment = Element.ALIGN_LEFT;
            adendoscell.Padding = 5;
            adendos.AddCell(adendoscell);

            PdfPTable adendosstring = new PdfPTable(1);
            adendosstring.WidthPercentage = 100;

            string adendostext =
                "Garantir redundância de energia e rede para evitar indisponibilidade.\n\n" +
                "Documentar toda a configuração realizada para futuras manutenções.\n\n" +
                "Treinar equipe interna sobre uso básico e procedimentos de contingência.\n\n";

            PdfPCell adendostextcell = new PdfPCell(new Phrase(adendostext, contentFont));
            adendostextcell.BackgroundColor = new BaseColor(240, 240, 240);
            adendostextcell.Border = Rectangle.BOTTOM_BORDER;
            adendostextcell.BorderColor = BaseColor.WHITE;
            adendostextcell.HorizontalAlignment = Element.ALIGN_LEFT;
            adendostextcell.Padding = 7;
            adendosstring.AddCell(adendostextcell);


            doc.Add(adendos);
            doc.Add(adendosstring);

            #endregion


            doc.NewPage();

            #region Referência Visual e Imagens do projeto

            Paragraph referenciavisu = new Paragraph(new Phrase("REFERÊNCIA VISUAL:", new Font(bf, 16f, Font.BOLD, BaseColor.BLACK)));
            referenciavisu.Alignment = Element.ALIGN_LEFT;
            doc.Add(referenciavisu);
            doc.Add(space);
            doc.Add(space);


            var exampleleft = Image.GetInstance(@"img/Example.jpg");
            exampleleft.ScalePercent(60f);

            var exampleright = Image.GetInstance(@"img/Example.jpg");
            exampleright.ScalePercent(60f);

            var exampledownleft = Image.GetInstance(@"img/Example.jpg");
            exampleright.ScalePercent(60f);

            var exampledownright = Image.GetInstance(@"img/Example.jpg");
            exampleright.ScalePercent(60f);


            Paragraph linha1 = new Paragraph();
            linha1.Alignment = Element.ALIGN_CENTER;

            linha1.Add(new Chunk(Image.GetInstance(@"img/Example.jpg"), 0, 0, true));
            linha1.Add(space);
            linha1.Add(space);
            linha1.Add(new Chunk(Image.GetInstance(@"img/Example.jpg"), 62, 0, true));

            doc.Add(linha1);

            #endregion

            doc.Close();

        }





    }
}

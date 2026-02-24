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



            #region Pedido Compra

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

        #endregion
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
                "Executar cabeamento estruturado para garantir maior estabilidade e velocidade.\n\n" +
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

        public static void GerarRelatorio2()
        {


            string file = @"pdf/Relatório Análise Nexus.pdf";
            Directory.CreateDirectory("pdf");

            Document doc = new Document(PageSize.A4, 20, 20, 20, 20);
            PdfWriter.GetInstance(doc, new FileStream(file, FileMode.Create));
            doc.Open();

            // ================= FONTES =================
            BaseFont bfRegular = BaseFont.CreateFont(
                @"C:\Windows\Fonts\arial.ttf",
                BaseFont.IDENTITY_H,
                BaseFont.EMBEDDED);

            BaseFont bfBold = BaseFont.CreateFont(
                @"C:\Windows\Fonts\ariblk.ttf",
                BaseFont.IDENTITY_H,
                BaseFont.EMBEDDED);

            Font normal = new Font(bfRegular, 9);
            Font bold = new Font(bfBold, 9);
            Font header = new Font(bfBold, 10);
            Font tituloFont = new Font(bfBold, 16, Font.NORMAL, BaseColor.RED);

            BaseColor cinzaClaro = new BaseColor(240, 240, 240);
            BaseColor cinzaHeader = new BaseColor(220, 220, 220);
            BaseColor cinzaBorda = new BaseColor(200, 200, 200);

            #region CABEÇALHO PRINCIPAL DA PÁGINA COM (LOGO, RELATÓRIO DE ANÁLISE, ORÇ)
            // ================= CABEÇALHO =================
            PdfPTable topo = new PdfPTable(3);
            topo.WidthPercentage = 100;
            topo.SetWidths(new float[] { 23, 47, 20 });

            Image logo = Image.GetInstance(@"img\astec.jpeg");
            logo.ScaleToFit(120, 45);

            topo.AddCell(new PdfPCell(logo)
            {
                Border = Rectangle.NO_BORDER,
                PaddingLeft = 5,
                BorderColorRight = BaseColor.RED,
                BorderWidthRight = 0.8f
            });

            topo.AddCell(new PdfPCell(new Phrase("RELATÓRIO DE ANÁLISE", tituloFont))
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE
            });

            topo.AddCell(new PdfPCell(new Phrase("Orç.", bold))
            {
                Border = Rectangle.BOX,
                BorderColor = cinzaBorda,
                FixedHeight = 30,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE
            });

            doc.Add(topo);

            #endregion

            // ===== LINHA SEPARADORA =====
            PdfPTable linha = new PdfPTable(1);
            linha.WidthPercentage = 100;
            linha.AddCell(new PdfPCell { Border = Rectangle.BOTTOM_BORDER, BorderColor = cinzaBorda, FixedHeight = 10 });

            doc.Add(linha);

            #region PRIMEIRA TABELA COM DADOS DO EQUIPAMENTO
            // ================= DADOS =================
            AddHeader(doc, "DADOS DO EQUIPAMENTO", header, cinzaHeader);

            PdfPTable dados = new PdfPTable(4);
            dados.WidthPercentage = 100;
            dados.SetWidths(new float[] { 25, 35, 27, 35 });

            AddRow(dados, "Modelo/Produto:", "SSW070171T5SZ", "Data de fabricação:", "24 U", bold, normal);
            AddRow(dados, "Código de produto:", "10233130", "Opcionais/Acessórios:", "Sem opcionais.", bold, normal);
            AddRow(dados, "Número de série:", "1099914752", "Data da análise:", "22/12/2025", bold, normal);

            doc.Add(dados);

            #endregion

            #region TABELA DA DESCRIÇÃO DOS DEFEITOS
            // ================= DEFEITOS =================
            AddHeader(doc, "DESCRIÇÃO DOS DEFEITOS", header, cinzaHeader);

            PdfPTable defeitos1 = new PdfPTable(2);
            defeitos1.WidthPercentage = 100;
            defeitos1.SetWidths(new float[] { 30, 70 });

            defeitos1.AddCell(LabelCell("Defeito(s) encontrado(s):", bold));
            defeitos1.AddCell(ValueCell(
                "• Cartão de potência e cartão de controle danificados, impedindo a energização do drive.",
                normal));

            doc.Add(defeitos1);

            #endregion

            #region TABELA DE FALHAS REGISTRADAS NA MEMÓRIA E DEFEITO INFORMADO PELO CLIENTE
            PdfPTable defeitos3 = new PdfPTable(2);
            defeitos3.WidthPercentage = 100;
            defeitos3.SetWidths(new float[] { 30, 70 });

            defeitos3.AddCell(LabelCell("Falhas registradas na memória:", bold));
            defeitos3.AddCell(ValueCell("Não registrado.", normal, true));

            defeitos3.AddCell(LabelCell("Defeito informado pelo cliente:", bold));
            defeitos3.AddCell(ValueCell("Não informado.", normal, true));

            doc.Add(defeitos3);

            #endregion

            #region TABELA TIPO DE DEFEITO COM 3 CHECKBOX APENAS (CONSTANTE, INTERMITENTE, SEM DEFEITO)
            // ===== TIPO DE DEFEITO (2 COLUNAS) =====
            PdfPTable defeitos2 = new PdfPTable(2);
            defeitos2.WidthPercentage = 100;
            defeitos2.SetWidths(new float[] { 30, 70 });

            PdfPCell tipo = LabelCell("Tipo de defeito:", bold);
            tipo.Rowspan = 3;
            tipo.VerticalAlignment = Element.ALIGN_MIDDLE;
            defeitos2.AddCell(tipo);

            defeitos2.AddCell(CelulaOpcaoDefeito("Constante", false, normal));
            defeitos2.AddCell(CelulaOpcaoDefeito("Intermitente", false, normal));
            defeitos2.AddCell(CelulaOpcaoDefeito("Sem defeito", false, normal));

            defeitos2.AddCell(LabelCell("Possíveis causas do(s) defeito(s):", bold));
            defeitos2.AddCell(new PdfPCell(CriarTabelaCausas(normal))
            {
                Padding = 6,
                BackgroundColor = cinzaClaro,
            });

            doc.Add(defeitos2);

            #endregion

            #region TABELA DE ATIVIDADES DE REPARO A SEREM EXECUTADAS
            // ================= ATIVIDADES =================
            AddHeader(doc, "ATIVIDADES DE REPARO A SEREM EXECUTADAS", header, cinzaHeader);

            PdfPTable atividades = new PdfPTable(1) { WidthPercentage = 100 };
            atividades.AddCell(ValueCell(
                "• Desmontagem completa do equipamento;\n\n" +
                "• Higienização/Limpeza das peças do equipamento;\n\n" +
                "• Secagem em estufa por meio de temperatura controlada;\n\n" +
                "• Substituição do cartão de controle;\n\n" +
                "• Substituição do cartão de potência;\n\n" +
                "• Reaplicação da pasta térmica;\n\n" +
                "• Revisão dos circuitos eletrônicos;\n\n" +
                "• Montagem do equipamento;\n\n" +
                "• Testes em laboratório.\n\n",
                normal));

            doc.Add(atividades);

            #endregion

            #region TABELA DE TÉCNICO RESPONSÁVEL E VALOR DO REPARO
            // ================= RODAPÉ =================
            PdfPTable rodape = new PdfPTable(2);
            rodape.WidthPercentage = 100;
            rodape.SetWidths(new float[] { 50, 50 });

            rodape.AddCell(LabelCell("Técnico Responsável:", bold));
            rodape.AddCell(ValueCell("Vinícius Torres", normal, true));

            rodape.AddCell(LabelCell("Valor do reparo:", bold));
            rodape.AddCell(ValueCell("R$ 3.327,72", normal, true));

            doc.Add(rodape);

            #endregion

            //==================Espaço=================
            Paragraph espaco1 = new Paragraph("\n\n\n\n");
            doc.Add(espaco1);

            #region TABELA OBSERVAÇÕES
            // ================= OBSERVAÇÕES =================
            AddHeader(doc, "OBSERVAÇÕES", tituloFont, cinzaHeader);

            PdfPTable obs = new PdfPTable(1) { WidthPercentage = 100 };
            obs.AddCell(new PdfPCell(new Phrase(
               "• O equipamento será enviado com a parametrização que chegou a Waldesa.\n\n" +
               "• O drive ficará disponível na Waldesa Automação por um período de 1 (um) mês aguardando o cliente se pronunciar, " +
               "caso contrário o equipamento será enviado de volta para o proprietário sem aviso prévio.\n\n" +
               "• Sugerimos a correta dimensionamento do drive e a reavaliação do local/ambiente de instalação, " +
               "a fim de garantir a máxima eficiência e desempenho do equipamento apresentado pelo manual do usuário, " +
               "tais como: ventilação; falta a terra; dispositivo de proteção adequado (disjuntor motor ou fusível ultrarrápido).", bold))
            { Padding = 8,
                HorizontalAlignment = Element.ALIGN_CENTER });

            doc.Add(obs);

            #endregion

            if (doc.PageNumber > 0)
                doc.NewPage();

            #region CABEÇALHO PÁGINA NOVA (RECEBIMENTO)
            // ================= CABEÇALHO PÁGINA NOVA (RECEBIMENTO) ================
            PdfPTable topo2 = new PdfPTable(2);
            topo2.WidthPercentage = 100;
            topo2.SetWidths(new float[] { 25, 74});

            Image logo2 = Image.GetInstance(@"img\astec.jpeg");
            logo2.ScaleToFit(120, 45);

            topo2.AddCell(new PdfPCell(logo2)
            {
                Border = Rectangle.NO_BORDER,
                PaddingLeft = 5,
                BorderColorRight = BaseColor.RED,
                BorderWidthRight = 0.8f
            });

            topo2.AddCell(new PdfPCell(new Phrase("     RELATÓRIO DE ANÁLISE", tituloFont))
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_LEFT,
                VerticalAlignment = Element.ALIGN_MIDDLE
            });

            doc.Add(topo2);
            doc.Add(linha);

            #endregion

            #region CÉLULA "REGISTRO DE FOTOS" CENTRALIZADA E COM BORDA
            // ================= SUBTÍTULO =================
            PdfPTable subtitulo = new PdfPTable(1);
            subtitulo.WidthPercentage = 100;
            subtitulo.HorizontalAlignment = Element.ALIGN_CENTER;

            subtitulo.AddCell(new PdfPCell(new Phrase("REGISTRO DE FOTOS", bold))
            {
                Border = Rectangle.BOX,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 6
            });

            doc.Add(subtitulo);

            #endregion

            // ================= ESPAÇO =================
            doc.Add(new Paragraph("\n"));

            #region TAG PÁGINA RECEBIMENTO
            // ================= TAG RECEBIMENTO =================
            PdfPTable tag = new PdfPTable(1);
            tag.WidthPercentage = 25;
            tag.HorizontalAlignment = Element.ALIGN_LEFT;

            tag.AddCell(new PdfPCell(new Phrase("RECEBIMENTO", bold))
            {
                BackgroundColor = BaseColor.WHITE,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5
            });

            doc.Add(tag);
            #endregion

            #region FOTO DE EXEMPLO DAS PÁGINAS DE FOTOS
            // ================= FOTO =================
            doc.Add(new Paragraph("\n"));

            Image foto = Image.GetInstance(@"Example.jpg");
            foto.ScaleToFit(450, 600);
            foto.Alignment = Element.ALIGN_CENTER;

            PdfPTable fotoBox = new PdfPTable(1);
            fotoBox.WidthPercentage = 100;

            fotoBox.AddCell(new PdfPCell(foto)
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5
            });

            doc.Add(fotoBox);

            #endregion

            #region RODAPÉ MENSAGEM ABAIXO DAS FOTOS
            // ================= RODAPÉ =================
            doc.Add(new Paragraph("\n\n"));

            Paragraph rodape2 = new Paragraph(
                "A conclusão quanto as possíveis causas foram baseadas na análise do produto em laboratório.",
                new Font(bfRegular, 8, Font.ITALIC)
            );
            rodape2.Alignment = Element.ALIGN_CENTER;

            doc.Add(rodape2);

            #endregion

            #region PÁGINA ETIQUETA

            doc.NewPage();

            // ================= CABEÇALHO PÁGINA NOVA (ETIQUETA) ================

            doc.Add(topo2);
            doc.Add(linha);

            // ================= ESPAÇO =================
            doc.Add(new Paragraph("\n"));

            // ================= TAG ETIQUETA =================
            PdfPTable tag2 = new PdfPTable(1);
            tag2.WidthPercentage = 25;
            tag2.HorizontalAlignment = Element.ALIGN_LEFT;

            tag2.AddCell(new PdfPCell(new Phrase("ETIQUETA", bold))
            {
                BackgroundColor = BaseColor.WHITE,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5
            });

            doc.Add(tag2);

            // ================= FOTO =================
            doc.Add(new Paragraph("\n"));

            doc.Add(fotoBox);

            // ================= RODAPÉ =================
    
            doc.Add(rodape2);

            #endregion

            #region PÁGINA DESMONTAGEM E EVIDÊNCIA DE DANO
            doc.NewPage();

            // ================= CABEÇALHO PÁGINA NOVA (DESMONTAGEM E EVIDÊNCIA DE DANO) ================

            doc.Add(topo2);
            doc.Add(linha);

            // ================= ESPAÇO =================
            doc.Add(new Paragraph("\n"));

            // ================= TAG ETIQUETA =================
            PdfPTable tag3 = new PdfPTable(1);
            tag3.WidthPercentage = 25;
            tag3.HorizontalAlignment = Element.ALIGN_LEFT;

            tag3.AddCell(new PdfPCell(new Phrase("DESMONTAGEM E EVIDÊNCIA DE DANO", bold))
            {
                BackgroundColor = BaseColor.WHITE,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5
            });

            doc.Add(tag3);

            // ================= FOTO =================
            doc.Add(new Paragraph("\n"));

            doc.Add(fotoBox);

            // ================= RODAPÉ =================

            doc.Add(rodape2);

            #endregion

            #region PÁGINA SUJIDADE INTERNA
            doc.NewPage();

            // ================= CABEÇALHO PÁGINA NOVA (SUJIDADE INTERNA) ================

            doc.Add(topo2);
            doc.Add(linha);

            // ================= ESPAÇO =================
            doc.Add(new Paragraph("\n"));

            // ================= TAG ETIQUETA =================
            PdfPTable tag4 = new PdfPTable(1);
            tag4.WidthPercentage = 25;
            tag4.HorizontalAlignment = Element.ALIGN_LEFT;

            tag4.AddCell(new PdfPCell(new Phrase("SUJIDADE INTERNA", bold))
            {
                BackgroundColor = BaseColor.WHITE,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 5
            });

            doc.Add(tag4);

            // ================= FOTO =================
            doc.Add(new Paragraph("\n"));

            doc.Add(fotoBox);

            // ================= RODAPÉ =================

            doc.Add(rodape2);





            doc.Close();
        }
        #endregion


        #region TUDO RELACIONADO AO CHECK BOX DA PRIMEIRA PÁGINA (TIPO DE DEFEITO E CAUSAS) COLUNA DIVIDIDA

        // ================= HELPERS =================

        static void AddHeader(Document doc, string text, Font font, BaseColor bg)
        {
            PdfPTable t = new PdfPTable(1) { WidthPercentage = 100 };
            t.AddCell(new PdfPCell(new Phrase(text, font))
            {
                BackgroundColor = bg,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Padding = 6
            });
            doc.Add(t);
        }

        static void AddRow(PdfPTable t, string l1, string v1, string l2, string v2, Font bold, Font normal)
        {
            t.AddCell(LabelCell(l1, bold));
            t.AddCell(ValueCell(v1, normal));
            t.AddCell(LabelCell(l2, bold));
            t.AddCell(ValueCell(v2, normal));
        }

        static PdfPCell LabelCell(string text, Font font) =>
            new PdfPCell(new Phrase(text, font))
            {
                Padding = 4,
                BackgroundColor = new BaseColor(245, 245, 245)
            };

        static PdfPCell ValueCell(string text, Font font, bool italico = false)
        {
            Font f = italico ? new Font(font.BaseFont, font.Size, Font.ITALIC) : font;
            return new PdfPCell(new Phrase(text, f)) { Padding = 4 };
        }

        // ===== CHECKBOX ESTÁTICO =====

        static PdfPCell CheckCell(bool marcado)
        {
            PdfPCell cell = new PdfPCell
            {
                Border = Rectangle.NO_BORDER,
                MinimumHeight = 14,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };
            cell.CellEvent = new StaticCheckboxCellEvent(marcado);
            return cell;
        }

        class StaticCheckboxCellEvent : IPdfPCellEvent
        {
            private readonly bool marcado;
            public StaticCheckboxCellEvent(bool marcado) => this.marcado = marcado;

            public void CellLayout(PdfPCell cell, Rectangle pos, PdfContentByte[] canvases)
            {
                PdfContentByte cb = canvases[PdfPTable.LINECANVAS];
                float size = 9;
                float x = pos.Left + (pos.Width - size) / 2;
                float y = pos.Bottom + (pos.Height - size) / 2;

                cb.Rectangle(x, y, size, size);
                cb.Stroke();

                if (marcado)
                {
                    cb.MoveTo(x + 1, y + 1);
                    cb.LineTo(x + size - 1, y + size - 1);
                    cb.MoveTo(x + 1, y + size - 1);
                    cb.LineTo(x + size - 1, y + 1);
                    cb.Stroke();
                }
            }
        }

        static PdfPCell CelulaOpcaoDefeito(string texto, bool marcado, Font normal)
        {
            PdfPTable t = new PdfPTable(2);
            t.SetWidths(new float[] { 8, 92 });
            t.WidthPercentage = 100;

            t.AddCell(CheckCell(marcado));
            t.AddCell(new PdfPCell(new Phrase(texto, normal))
            {
                Border = Rectangle.RIGHT_BORDER,
                VerticalAlignment = Element.ALIGN_MIDDLE
            });

            return new PdfPCell(t) { Border = Rectangle.NO_BORDER };
        }

        static PdfPTable CriarTabelaCausas(Font normal)
        {
            string[] esq =
            {
            "Vida útil","Curto-circuito","Oscilação na rede","Sobretensão",
            "Sub tensão","Sobrecarga","Sobrecorrente","Sobtemperatura","Sujidade"
        };

            string[] dir =
            {
            "Travamento","Intervenção técnica","Ambiente agressivo",
            "Dif. potencial terra/0V","Má conexão","Choque mecânico",
            "Inversão cabos","Descarga atmosférica","Não evidenciada"
        };

            PdfPTable t = new PdfPTable(2) { WidthPercentage = 100 };
            int linhas = Math.Max(esq.Length, dir.Length);

            for (int i = 0; i < linhas; i++)
            {
                t.AddCell(i < esq.Length ? LinhaCausa(esq[i], normal) : CelulaVazia());
                t.AddCell(i < dir.Length ? LinhaCausa(dir[i], normal) : CelulaVazia());
            }
            return t;
        }

        static PdfPCell LinhaCausa(string texto, Font normal)
        {
            PdfPTable t = new PdfPTable(2);
            t.SetWidths(new float[] { 10, 90 });
            t.AddCell(CheckCell(false));
            t.AddCell(new PdfPCell(new Phrase(texto, normal)) { Border = Rectangle.NO_BORDER });
            return new PdfPCell(t) { Border = Rectangle.NO_BORDER };
        }

        static PdfPCell CelulaVazia() =>
            new PdfPCell { Border = Rectangle.NO_BORDER };
    }

}

#endregion
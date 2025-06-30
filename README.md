# 🍎 FruitDetector_SS2023

Sistema de visão computacional desenvolvido em **C# com Emgu.CV** (OpenCV para .NET), no âmbito da unidade curricular de **Sistemas Sensoriais** do 4º ano do Mestrado Integrado em Engenharia Eletrotécnica e de Computadores.

O sistema deteta e identifica frutas automaticamente a partir de imagens, analisando características morfológicas e de cor.

---

##  Objetivo

Desenvolver uma aplicação capaz de:
- Processar imagens contendo frutas
- Detetar os objetos/frutas presentes
- Calcular características como área, perímetro, circularidade, cor média, etc.
- Comparar essas métricas com uma base de dados de referência
- Identificar automaticamente cada fruta com base em critérios quantitativos

---

##  Principais Funcionalidades

- Binarização automática com **Otsu**
- Filtros morfológicos (erosão, dilatação)
- Deteção de **componentes ligados**
- Cálculo de:
  - Área real (cm²)
  - Perímetro
  - Circularidade
  - Centroide
  - Quociente de aspeto
  - Fator de forma
  - Cor média (vermelho e verde)
- Conversão de pixels para medidas reais (cm), usando objeto de calibração (losango)
- Identificação da fruta com base em diferenças ponderadas face à tabela de referência
- Desenho de bounding boxes e retângulos nas frutas detetadas

---

##  Interface

A interface gráfica foi desenvolvida com **Windows Forms**, permitindo:
- Carregamento de imagens
- Execução de filtros e operações
- Visualização das métricas em tempo real
- Interação através de menus suspensos

---

##  Estrutura do Projeto

FruitDetector_SS2023/
│
├── SS_OpenCV_2023.sln # Solução Visual Studio

├── SS_OpenCV/ # Projeto principal
│ ├── MainForm.cs # Interface gráfica e eventos
│ ├── ImageClass.cs # Lógica de processamento (FruitReader, métricas, etc.)

├── FruitDLL/ # (Opcional) Biblioteca auxiliar

├── pack_imagens/ # Imagens de teste

├── SS_operations.csv # Exportação de dados (opcional)

├── vcredist_x86_2012.exe # Redistributable necessário para EmguCV

└── README.md # Este ficheiro


## 🚀 Como correr o projeto

1. Clonar este repositório:
   ```bash
   git clone https://github.com/Alves1306/FruitDetector_SS2023.git
Abreir o projeto:

Abre o SS_OpenCV_2023.sln no Visual Studio.

Verificar os pacotes NuGet:

Confirmar que se tem Emgu.CV e dependências instaladas.

Compilar.

Corre com F5 e carrega uma imagem com frutas.

IR ao menu “Fruit Reader” para iniciar o processamento.

📊 Exemplo de resultado
Frutas contornadas com retângulos

Nome da fruta e medidas exibidas no console

Métricas calculadas com base real (em cm)

Identificação feita com base em comparação a base de dados interna (Fruit_table)

👥 Autores
Projeto desenvolvido por:

António Alves — nº 58339

Márcio Costa — nº 60446

📚 Licença
Este projeto é de uso académico e não possui licença pública.
Uso restrito a fins didáticos.

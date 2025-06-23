-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Tempo de geração: 23/06/2025 às 06:03
-- Versão do servidor: 10.4.32-MariaDB
-- Versão do PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Banco de dados: `delsasso_stock`
--

-- --------------------------------------------------------

--
-- Estrutura para tabela `clients`
--

CREATE TABLE `clients` (
  `Id` char(36) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Cpf` varchar(14) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `clients`
--

INSERT INTO `clients` (`Id`, `Name`, `Cpf`) VALUES
('b541899b-4fad-11f0-8c13-74563ce7fd7b', 'Ana Paula Souza', '12345678909'),
('b541912c-4fad-11f0-8c13-74563ce7fd7b', 'Bruno Silva', '98765432100'),
('b5419184-4fad-11f0-8c13-74563ce7fd7b', 'Carlos Eduardo', '11144477735'),
('b541919e-4fad-11f0-8c13-74563ce7fd7b', 'Daniela Martins', '22233344405'),
('b54191b3-4fad-11f0-8c13-74563ce7fd7b', 'Eduardo Lima', '33322211196'),
('b54191c9-4fad-11f0-8c13-74563ce7fd7b', 'Fernanda Castro', '44455566677'),
('b54191db-4fad-11f0-8c13-74563ce7fd7b', 'Gabriel Almeida', '55566677788'),
('b54191ec-4fad-11f0-8c13-74563ce7fd7b', 'Helena Oliveira', '66677788899'),
('b5419201-4fad-11f0-8c13-74563ce7fd7b', 'Isabela Pereira', '77788899900'),
('b5419213-4fad-11f0-8c13-74563ce7fd7b', 'João Pedro Santos', '88899900011');

-- --------------------------------------------------------

--
-- Estrutura para tabela `products`
--

CREATE TABLE `products` (
  `Id` char(36) NOT NULL,
  `Name` varchar(100) NOT NULL,
  `Quantity` int(100) NOT NULL,
  `Price` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `products`
--

INSERT INTO `products` (`Id`, `Name`, `Quantity`, `Price`) VALUES
('e4877bd9-4fad-11f0-8c13-74563ce7fd7b', 'Caneta Azul', 92, 2.50),
('e487858d-4fad-11f0-8c13-74563ce7fd7b', 'Caderno Universitário', 47, 15.90),
('e4878605-4fad-11f0-8c13-74563ce7fd7b', 'Lápis HB', 197, 1.20),
('e487862f-4fad-11f0-8c13-74563ce7fd7b', 'Borracha Branca', 154, 0.99),
('e487864c-4fad-11f0-8c13-74563ce7fd7b', 'Mochila Escolar', 30, 89.90),
('e4878666-4fad-11f0-8c13-74563ce7fd7b', 'Estojo Simples', 82, 7.50),
('e4878680-4fad-11f0-8c13-74563ce7fd7b', 'Régua 30cm', 120, 3.40),
('e4878695-4fad-11f0-8c13-74563ce7fd7b', 'Apontador Duplo', 90, 2.80),
('e48786af-4fad-11f0-8c13-74563ce7fd7b', 'Marca Texto', 60, 4.99),
('e48786c6-4fad-11f0-8c13-74563ce7fd7b', 'Cola Bastão', 110, 3.75);

-- --------------------------------------------------------

--
-- Estrutura para tabela `saleitem`
--

CREATE TABLE `saleitem` (
  `Id` char(36) NOT NULL,
  `SaleId` char(36) NOT NULL,
  `ProductItemId` char(36) NOT NULL,
  `Quantity` int(11) NOT NULL,
  `UnitPrice` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `saleitem`
--

INSERT INTO `saleitem` (`Id`, `SaleId`, `ProductItemId`, `Quantity`, `UnitPrice`) VALUES
('fb6ca079-9e17-4d3b-bc1c-3ab4eccb790f', '30f5cd18-982c-4d8f-a655-a0bc6af1684a', 'e487862f-4fad-11f0-8c13-74563ce7fd7b', 2, 0.99);

-- --------------------------------------------------------

--
-- Estrutura para tabela `sales`
--

CREATE TABLE `sales` (
  `Id` char(36) NOT NULL,
  `CustomerId` char(36) DEFAULT NULL,
  `TotalSale` decimal(10,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `sales`
--

INSERT INTO `sales` (`Id`, `CustomerId`, `TotalSale`) VALUES
('30f5cd18-982c-4d8f-a655-a0bc6af1684a', 'b541899b-4fad-11f0-8c13-74563ce7fd7b', 1.98);

--
-- Índices para tabelas despejadas
--

--
-- Índices de tabela `clients`
--
ALTER TABLE `clients`
  ADD PRIMARY KEY (`Id`);

--
-- Índices de tabela `products`
--
ALTER TABLE `products`
  ADD PRIMARY KEY (`Id`);

--
-- Índices de tabela `saleitem`
--
ALTER TABLE `saleitem`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `fk_sale_items_sale` (`SaleId`),
  ADD KEY `fk_sale_items_product` (`ProductItemId`);

--
-- Índices de tabela `sales`
--
ALTER TABLE `sales`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `fk_sales_customer` (`CustomerId`);

--
-- Restrições para tabelas despejadas
--

--
-- Restrições para tabelas `saleitem`
--
ALTER TABLE `saleitem`
  ADD CONSTRAINT `fk_sale_items_product` FOREIGN KEY (`ProductItemId`) REFERENCES `products` (`Id`),
  ADD CONSTRAINT `fk_sale_items_sale` FOREIGN KEY (`SaleId`) REFERENCES `sales` (`id`) ON DELETE CASCADE;

--
-- Restrições para tabelas `sales`
--
ALTER TABLE `sales`
  ADD CONSTRAINT `fk_sales_customer` FOREIGN KEY (`CustomerId`) REFERENCES `clients` (`Id`) ON DELETE SET NULL;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

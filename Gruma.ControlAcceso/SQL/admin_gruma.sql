-- phpMyAdmin SQL Dump
-- version 4.7.8
-- https://www.phpmyadmin.net/
--
-- Host: localhost:3306
-- Generation Time: Dec 04, 2018 at 05:29 AM
-- Server version: 5.7.21
-- PHP Version: 7.1.14

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `admin_gruma`
--

-- --------------------------------------------------------

--
-- Table structure for table `categoria`
--

CREATE TABLE `categoria` (
  `id` int(11) NOT NULL,
  `nombre` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `curso`
--

CREATE TABLE `curso` (
  `id` int(11) NOT NULL,
  `nombre` varchar(250) DEFAULT NULL,
  `clave` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `cursosexternos`
--

CREATE TABLE `cursosexternos` (
  `id` int(11) NOT NULL,
  `participante_id` int(11) NOT NULL,
  `nombre` varchar(4000) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `curso_categoria`
--

CREATE TABLE `curso_categoria` (
  `curso_id` int(11) NOT NULL,
  `categoria_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `empresa`
--

CREATE TABLE `empresa` (
  `id` int(11) NOT NULL,
  `nombre` varchar(250) NOT NULL,
  `usuarioCanvas` varchar(50) DEFAULT NULL,
  `password` varchar(250) DEFAULT NULL,
  `salt` varchar(50) DEFAULT NULL,
  `rfc` varchar(20) DEFAULT NULL,
  `nombreFactura` varchar(250) DEFAULT NULL,
  `direccionFactura` varchar(500) DEFAULT NULL,
  `numeroProveedor` int(11) DEFAULT NULL,
  `nombreResponsable` varchar(150) DEFAULT NULL,
  `apPatResponsable` varchar(150) DEFAULT NULL,
  `apMatResponsable` varchar(150) DEFAULT NULL,
  `puestoResponsable` varchar(100) DEFAULT NULL,
  `telOficinaResponsable` varchar(50) DEFAULT NULL,
  `telCelularResponsable` varchar(50) DEFAULT NULL,
  `correoResponsable` varchar(100) DEFAULT NULL,
  `correoResponsable2` varchar(100) DEFAULT NULL,
  `telefonoFactura` varchar(50) DEFAULT NULL,
  `correoFactura` varchar(50) DEFAULT NULL,
  `activa` int(1) NOT NULL,
  `contrasenaCanvas` varchar(50) DEFAULT NULL,
  `observaciones` varchar(4000) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `estatus`
--

CREATE TABLE `estatus` (
  `id` int(11) NOT NULL,
  `descripcion` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `estatus`
--

INSERT INTO `estatus` (`id`, `descripcion`) VALUES
(1, 'Pendiente de Pago'),
(2, 'Tomando Curso'),
(3, 'Listo para examen'),
(4, 'Certificado entregado');

-- --------------------------------------------------------

--
-- Table structure for table `participante`
--

CREATE TABLE `participante` (
  `id` int(11) NOT NULL,
  `empresa_id` int(11) NOT NULL,
  `nombre` varchar(150) NOT NULL,
  `apellidoPaterno` varchar(150) DEFAULT NULL,
  `apellidoMaterno` varchar(150) DEFAULT NULL,
  `correoElectronico` varchar(150) DEFAULT NULL,
  `telefono` varchar(50) DEFAULT NULL,
  `certificaciones` varchar(500) DEFAULT NULL,
  `direccion` varchar(500) DEFAULT NULL,
  `fechaNacimiento` date DEFAULT NULL,
  `foto` varchar(500) DEFAULT NULL,
  `RFC` varchar(50) DEFAULT NULL,
  `CURP` varchar(50) DEFAULT NULL,
  `IMSS` varchar(50) DEFAULT NULL,
  `cartaNoAntecedentes` varchar(500) DEFAULT NULL,
  `registroIMSS` varchar(500) DEFAULT NULL,
  `usuarioCanvas` varchar(50) DEFAULT NULL,
  `passwordCanvas` varchar(20) DEFAULT NULL,
  `matricula` varchar(20) DEFAULT NULL,
  `credencial` bit(1) DEFAULT NULL,
  `vetado` bit(1) DEFAULT NULL,
  `motivo_veto` varchar(4000) DEFAULT NULL,
  `pago_imss_cadena` varchar(4000) DEFAULT NULL,
  `pago_imss_archivo` varchar(4000) DEFAULT NULL,
  `activo` bit(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `participante_certif`
--

CREATE TABLE `participante_certif` (
  `id` int(11) NOT NULL,
  `participante_id` int(11) NOT NULL,
  `fechaExamen` datetime DEFAULT NULL,
  `salaExamen` varchar(100) DEFAULT NULL,
  `certificacionObtenida` varchar(100) DEFAULT NULL,
  `calificacion` int(11) DEFAULT NULL,
  `fechaValidez` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `participante_curso`
--

CREATE TABLE `participante_curso` (
  `participante_id` int(11) NOT NULL,
  `curso_id` int(11) NOT NULL,
  `estatus_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `categoria`
--
ALTER TABLE `categoria`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `curso`
--
ALTER TABLE `curso`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `cursosexternos`
--
ALTER TABLE `cursosexternos`
  ADD PRIMARY KEY (`id`),
  ADD KEY `participante_id` (`participante_id`);

--
-- Indexes for table `curso_categoria`
--
ALTER TABLE `curso_categoria`
  ADD KEY `curso_id` (`curso_id`),
  ADD KEY `categoria_id` (`categoria_id`);

--
-- Indexes for table `empresa`
--
ALTER TABLE `empresa`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `estatus`
--
ALTER TABLE `estatus`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `participante`
--
ALTER TABLE `participante`
  ADD PRIMARY KEY (`id`),
  ADD KEY `empresa_id` (`empresa_id`);

--
-- Indexes for table `participante_certif`
--
ALTER TABLE `participante_certif`
  ADD PRIMARY KEY (`id`),
  ADD KEY `participante_id` (`participante_id`);

--
-- Indexes for table `participante_curso`
--
ALTER TABLE `participante_curso`
  ADD UNIQUE KEY `IDX_PARTICIPANTE_CURSO` (`participante_id`,`curso_id`),
  ADD KEY `curso_id` (`curso_id`),
  ADD KEY `estatus_id` (`estatus_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `categoria`
--
ALTER TABLE `categoria`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `curso`
--
ALTER TABLE `curso`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `cursosexternos`
--
ALTER TABLE `cursosexternos`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `empresa`
--
ALTER TABLE `empresa`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `estatus`
--
ALTER TABLE `estatus`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `participante`
--
ALTER TABLE `participante`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `participante_certif`
--
ALTER TABLE `participante_certif`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `cursosexternos`
--
ALTER TABLE `cursosexternos`
  ADD CONSTRAINT `cursosexternos_ibfk_1` FOREIGN KEY (`participante_id`) REFERENCES `participante` (`id`);

--
-- Constraints for table `curso_categoria`
--
ALTER TABLE `curso_categoria`
  ADD CONSTRAINT `curso_categoria_ibfk_1` FOREIGN KEY (`curso_id`) REFERENCES `curso` (`id`),
  ADD CONSTRAINT `curso_categoria_ibfk_2` FOREIGN KEY (`categoria_id`) REFERENCES `categoria` (`id`);

--
-- Constraints for table `participante`
--
ALTER TABLE `participante`
  ADD CONSTRAINT `participante_ibfk_1` FOREIGN KEY (`empresa_id`) REFERENCES `empresa` (`id`);

--
-- Constraints for table `participante_certif`
--
ALTER TABLE `participante_certif`
  ADD CONSTRAINT `participante_certif_ibfk_1` FOREIGN KEY (`participante_id`) REFERENCES `participante` (`id`);

--
-- Constraints for table `participante_curso`
--
ALTER TABLE `participante_curso`
  ADD CONSTRAINT `participante_curso_ibfk_1` FOREIGN KEY (`curso_id`) REFERENCES `curso` (`id`),
  ADD CONSTRAINT `participante_curso_ibfk_2` FOREIGN KEY (`estatus_id`) REFERENCES `estatus` (`id`),
  ADD CONSTRAINT `participante_curso_ibfk_3` FOREIGN KEY (`participante_id`) REFERENCES `participante` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

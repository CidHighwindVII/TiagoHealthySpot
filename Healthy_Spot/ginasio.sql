CREATE DATABASE  IF NOT EXISTS `pap_ginasio` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `pap_ginasio`;
-- MySQL dump 10.13  Distrib 8.0.30, for Win64 (x86_64)
--
-- Host: localhost    Database: pap_ginasio
-- ------------------------------------------------------
-- Server version	8.0.30

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `equipamentos`
--

DROP TABLE IF EXISTS `equipamentos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `equipamentos` (
  `IdMaquina` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(75) NOT NULL,
  `TipoMaquina` varchar(20) NOT NULL,
  `MusculoMaquina` varchar(20) NOT NULL,
  `FotoMaquina` varchar(255) NOT NULL,
  PRIMARY KEY (`IdMaquina`),
  KEY `Nome_idx` (`Nome`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `equipamentos`
--

LOCK TABLES `equipamentos` WRITE;
/*!40000 ALTER TABLE `equipamentos` DISABLE KEYS */;
INSERT INTO `equipamentos` VALUES (2,'Passadeira automáticassss','Passadeira','Pernas','\\Content\\Imagens\\20230130092112 - passadeira.png'),(3,'Bicicleta eliptica','Bicicleta','Pernas','\\Content\\Imagens\\20230223210624 - bike.png');
/*!40000 ALTER TABLE `equipamentos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `exercicio`
--

DROP TABLE IF EXISTS `exercicio`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `exercicio` (
  `IdExercicio` int NOT NULL AUTO_INCREMENT,
  `NomeExercicio` varchar(45) NOT NULL,
  `IdMaquina` int NOT NULL,
  PRIMARY KEY (`IdExercicio`),
  KEY `IdMaquina_idx` (`IdMaquina`),
  CONSTRAINT `IdMaquina` FOREIGN KEY (`IdMaquina`) REFERENCES `equipamentos` (`IdMaquina`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `exercicio`
--

LOCK TABLES `exercicio` WRITE;
/*!40000 ALTER TABLE `exercicio` DISABLE KEYS */;
INSERT INTO `exercicio` VALUES (1,'Corrida 2 km',2);
/*!40000 ALTER TABLE `exercicio` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `utilizadores`
--

DROP TABLE IF EXISTS `utilizadores`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `utilizadores` (
  `IdUtilizador` int NOT NULL AUTO_INCREMENT,
  `Email` varchar(70) NOT NULL,
  `Password` varchar(70) NOT NULL,
  `Nome` varchar(45) NOT NULL,
  `Localidade` varchar(45) NOT NULL,
  `Sexo` varchar(45) NOT NULL,
  `DataNascimento` date NOT NULL,
  `Foto` varchar(255) DEFAULT NULL,
  `hashLink` varchar(45) NOT NULL,
  `dataRegisto` datetime NOT NULL,
  `ativo` tinyint NOT NULL,
  PRIMARY KEY (`IdUtilizador`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `utilizadores`
--

LOCK TABLES `utilizadores` WRITE;
/*!40000 ALTER TABLE `utilizadores` DISABLE KEYS */;
INSERT INTO `utilizadores` VALUES (7,'a14627@aedah.pt','81dc9bdb52d04dc20036dbd8313ed055','Tiago Leite','Vila das Aves','Masculino','2000-01-01','\\Content\\Imagens\\20230313182702 - 04.PNG','BB0137DE7EEEB4CCE6CEF6936D99E6A5AD8F4832','2023-03-13 18:27:03',0);
/*!40000 ALTER TABLE `utilizadores` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-03-14  8:35:19

-- =================================================================================
-- MySQL Database Creation & Table Schema Script for balajiPortfolio
-- =================================================================================

-- 1. Create Database if it does not exist
CREATE DATABASE IF NOT EXISTS `balajiPortfolio` 
DEFAULT CHARACTER SET utf8mb4 
COLLATE utf8mb4_unicode_ci;

USE `balajiPortfolio`;

-- =================================================================================
-- 2. Table: mstPersonal (Personal Profile, About Section & Social Media Links)
-- =================================================================================
CREATE TABLE IF NOT EXISTS `mstPersonal` (
    `mstPersonalId` INT NOT NULL AUTO_INCREMENT,
    `name` VARCHAR(255) NULL,
    `profilePictureUrl` VARCHAR(500) NULL,
    `mobileNumber` VARCHAR(50) NULL,
    `email` VARCHAR(255) NULL,
    `location` VARCHAR(255) NULL,
    `bio` TEXT NULL,
    `workDetails` VARCHAR(255) NULL,
    `professionalSummary` TEXT NULL,
    `yearsOfExperience` VARCHAR(50) NULL,
    `projectsCompleted` VARCHAR(50) NULL,
    `companiesWorked` VARCHAR(50) NULL,
    `linkedInUrl` VARCHAR(500) NULL,
    `gitHubUrl` VARCHAR(500) NULL,
    `gitLabUrl` VARCHAR(500) NULL,
    `orcidUrl` VARCHAR(500) NULL,
    `googleScholarUrl` VARCHAR(500) NULL,
    `isActive` TINYINT(1) NOT NULL DEFAULT 1,
    `updatedAt` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (`mstPersonalId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =================================================================================
-- 3. Table: mstExperience (Work Experience & Dynamic Responsibilities)
-- =================================================================================
CREATE TABLE IF NOT EXISTS `mstExperience` (
    `mstExperienceId` INT NOT NULL AUTO_INCREMENT,
    `jobTitle` VARCHAR(255) NULL,
    `company` VARCHAR(255) NULL,
    `location` VARCHAR(255) NULL,
    `startDate` VARCHAR(100) NULL,
    `endDate` VARCHAR(100) NULL,
    `isCurrentlyWorking` TINYINT(1) NOT NULL DEFAULT 0,
    `responsibilities` TEXT NULL,
    `isActive` TINYINT(1) NOT NULL DEFAULT 1,
    `updatedAt` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (`mstExperienceId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =================================================================================
-- 4. Table: mstSkillCategory (Skill Categories)
-- =================================================================================
CREATE TABLE IF NOT EXISTS `mstSkillCategory` (
    `mstSkillCategoryId` INT NOT NULL AUTO_INCREMENT,
    `categoryName` VARCHAR(255) NULL,
    `isActive` TINYINT(1) NOT NULL DEFAULT 1,
    `updatedAt` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (`mstSkillCategoryId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =================================================================================
-- 5. Table: mstSkill (Technical Skills mapped to mstSkillCategory)
-- =================================================================================
CREATE TABLE IF NOT EXISTS `mstSkill` (
    `mstSkillId` INT NOT NULL AUTO_INCREMENT,
    `mstSkillCategoryId` INT NOT NULL,
    `skillName` VARCHAR(255) NULL,
    `proficiencyLevel` VARCHAR(100) NULL,
    `isActive` TINYINT(1) NOT NULL DEFAULT 1,
    `updatedAt` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (`mstSkillId`),
    KEY `IX_mstSkill_mstSkillCategoryId` (`mstSkillCategoryId`),
    CONSTRAINT `FK_mstSkill_mstSkillCategory_mstSkillCategoryId` 
        FOREIGN KEY (`mstSkillCategoryId`) 
        REFERENCES `mstSkillCategory` (`mstSkillCategoryId`) 
        ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =================================================================================
-- 6. Table: mstProject (Projects Showcase with Product Image & CAD File)
-- =================================================================================
CREATE TABLE IF NOT EXISTS `mstProject` (
    `mstProjectId` INT NOT NULL AUTO_INCREMENT,
    `projectTitle` VARCHAR(255) NULL,
    `description` TEXT NULL,
    `productImageUrl` VARCHAR(500) NULL,
    `cadFilePath` VARCHAR(500) NULL,
    `tags` TEXT NULL,
    `isActive` TINYINT(1) NOT NULL DEFAULT 1,
    `updatedAt` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (`mstProjectId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =================================================================================
-- 7. Table: mstCertification (Certifications & Badges)
-- =================================================================================
CREATE TABLE IF NOT EXISTS `mstCertification` (
    `mstCertificationId` INT NOT NULL AUTO_INCREMENT,
    `type` VARCHAR(100) NULL,
    `name` VARCHAR(255) NULL,
    `issuer` VARCHAR(255) NULL,
    `issueDate` VARCHAR(100) NULL,
    `credentialLink` VARCHAR(500) NULL,
    `embedCode` TEXT NULL,
    `isActive` TINYINT(1) NOT NULL DEFAULT 1,
    `updatedAt` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (`mstCertificationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- =================================================================================
-- 8. Table: mstPublication (Publications & Research Papers)
-- =================================================================================
CREATE TABLE IF NOT EXISTS `mstPublication` (
    `mstPublicationId` INT NOT NULL AUTO_INCREMENT,
    `title` VARCHAR(255) NULL,
    `publisher` VARCHAR(255) NULL,
    `publicationDate` VARCHAR(100) NULL,
    `publicationLink` VARCHAR(500) NULL,
    `isActive` TINYINT(1) NOT NULL DEFAULT 1,
    `updatedAt` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (`mstPublicationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

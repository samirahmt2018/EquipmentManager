-- phpMyAdmin SQL Dump
-- version 4.8.3
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Nov 13, 2018 at 10:14 PM
-- Server version: 10.1.36-MariaDB
-- PHP Version: 7.2.10

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+03:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `flintstones_db`
--
CREATE DATABASE IF NOT EXISTS `flintstones_db` DEFAULT CHARACTER SET utf8 COLLATE utf8_unicode_520_ci;
USE `flintstones_db`;

-- --------------------------------------------------------

--
-- Table structure for table `allocate_item`
--

CREATE TABLE `allocate_item` (
  `id` int(11) NOT NULL,
  `request_id` int(11) NOT NULL,
  `equipment_id` varchar(50) COLLATE utf8_unicode_520_ci NOT NULL,
  `allocate_from` int(11) NOT NULL,
  `allocate_to` int(11) NOT NULL,
  `transportation_cost` decimal(10,0) NOT NULL,
  `allocation_date` date NOT NULL,
  `om_approved` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_520_ci;

-- --------------------------------------------------------

--
-- Table structure for table `allocation_request`
--

CREATE TABLE `allocation_request` (
  `id` int(11) NOT NULL,
  `machine_id` int(11) NOT NULL,
  `unit` varchar(30) COLLATE utf8_unicode_520_ci NOT NULL,
  `quantity` int(11) NOT NULL,
  `site_id` int(11) NOT NULL,
  `delivery_date` date NOT NULL,
  `duration` int(11) NOT NULL,
  `work_type` varchar(100) COLLATE utf8_unicode_520_ci NOT NULL,
  `work_zone` varchar(150) COLLATE utf8_unicode_520_ci NOT NULL,
  `volume_of_work` varchar(100) COLLATE utf8_unicode_520_ci NOT NULL,
  `requested_by` varchar(15) COLLATE utf8_unicode_520_ci NOT NULL,
  `remark` varchar(200) COLLATE utf8_unicode_520_ci NOT NULL,
  `request_status` int(11) NOT NULL DEFAULT '1',
  `allocated_item_no` int(11) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_520_ci;

-- --------------------------------------------------------

--
-- Table structure for table `department`
--

CREATE TABLE `department` (
  `id` int(11) NOT NULL,
  `department_name` varchar(100) COLLATE utf8_unicode_520_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_520_ci;

--
-- Dumping data for table `department`
--

INSERT INTO `department` (`id`, `department_name`) VALUES
(1, 'Human Resource'),
(2, 'Finance'),
(3, 'Equipment'),
(4, 'Engineering'),
(5, 'Drivers'),
(6, 'Technicians'),
(7, 'Contract'),
(8, 'Marketing'),
(9, 'Operators');

-- --------------------------------------------------------

--
-- Table structure for table `equipment_list`
--

CREATE TABLE `equipment_list` (
  `equipment_id` varchar(50) COLLATE utf8_unicode_520_ci NOT NULL,
  `machine_id` int(11) NOT NULL,
  `plate_number` varchar(50) COLLATE utf8_unicode_520_ci NOT NULL,
  `manufactured_year` year(4) NOT NULL,
  `current_location` int(11) NOT NULL,
  `current_status` int(11) NOT NULL,
  `Investment_cost` decimal(10,0) NOT NULL,
  `serviced_every` int(11) NOT NULL,
  `equipment_type` int(11) NOT NULL,
  `insurance_status` int(11) NOT NULL,
  `operator_id` varchar(15) COLLATE utf8_unicode_520_ci DEFAULT NULL,
  `registered_date` date NOT NULL,
  `depreciated_cost` decimal(10,0) NOT NULL DEFAULT '0',
  `supplied_by` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_520_ci;

-- --------------------------------------------------------

--
-- Table structure for table `equipment_productivity_control`
--

CREATE TABLE `equipment_productivity_control` (
  `id` int(11) NOT NULL,
  `Date` date NOT NULL,
  `equipment_id` varchar(50) COLLATE utf8_unicode_520_ci NOT NULL,
  `activity_detail` varchar(300) COLLATE utf8_unicode_520_ci NOT NULL,
  `morning_start_at` decimal(10,0) NOT NULL,
  `morning_end_at` decimal(10,0) NOT NULL,
  `afternoon_start_at` decimal(10,0) NOT NULL,
  `afternoon_end_at` decimal(10,0) NOT NULL,
  `Idle_time` float NOT NULL,
  `idle_reason` varchar(150) COLLATE utf8_unicode_520_ci NOT NULL,
  `fuel_used` decimal(10,0) NOT NULL,
  `site_productivity` decimal(10,0) NOT NULL,
  `user_id` varchar(15) COLLATE utf8_unicode_520_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_520_ci;

-- --------------------------------------------------------

--
-- Table structure for table `equipment_purchase_request`
--

CREATE TABLE `equipment_purchase_request` (
  `id` int(11) NOT NULL,
  `group_id` varchar(30) COLLATE utf8_unicode_520_ci NOT NULL,
  `machine_id` int(11) NOT NULL,
  `unit` varchar(30) COLLATE utf8_unicode_520_ci NOT NULL,
  `quantity` int(11) NOT NULL,
  `site_id` int(11) NOT NULL,
  `reason` varchar(500) COLLATE utf8_unicode_520_ci NOT NULL,
  `price` decimal(10,0) NOT NULL,
  `remarks` varchar(500) COLLATE utf8_unicode_520_ci NOT NULL,
  `om_approved` tinyint(1) NOT NULL,
  `om_remarks` varchar(500) COLLATE utf8_unicode_520_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_520_ci;

-- --------------------------------------------------------

--
-- Table structure for table `equipment_status`
--

CREATE TABLE `equipment_status` (
  `status_id` int(11) NOT NULL,
  `status_name` varchar(20) COLLATE utf8_unicode_520_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_520_ci;

--
-- Dumping data for table `equipment_status`
--

INSERT INTO `equipment_status` (`status_id`, `status_name`) VALUES
(1, 'In Store'),
(2, 'Working'),
(3, 'On Maintainance'),
(4, 'Not Working'),
(5, 'Sold'),
(6, 'Renturned'),
(7, 'Being Serviced');

-- --------------------------------------------------------

--
-- Table structure for table `equipment_type`
--

CREATE TABLE `equipment_type` (
  `id` int(11) NOT NULL,
  `type_name` varchar(50) COLLATE utf8_unicode_520_ci NOT NULL,
  `equipment_type_abbr` varchar(2) COLLATE utf8_unicode_520_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_520_ci;

--
-- Dumping data for table `equipment_type`
--

INSERT INTO `equipment_type` (`id`, `type_name`, `equipment_type_abbr`) VALUES
(3, 'Rented', 'RT'),
(4, 'Purchased', 'PR');

-- --------------------------------------------------------

--
-- Table structure for table `insurance_payment`
--

CREATE TABLE `insurance_payment` (
  `id` int(11) NOT NULL,
  `equipment_id` varchar(50) COLLATE utf8_unicode_520_ci NOT NULL,
  `value_payed` decimal(10,0) NOT NULL,
  `payment_date` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_520_ci;

-- --------------------------------------------------------

--
-- Table structure for table `insurance_statuses`
--

CREATE TABLE `insurance_statuses` (
  `id` int(11) NOT NULL,
  `insurance_status_name` varchar(25) COLLATE utf8_unicode_520_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_520_ci;

--
-- Dumping data for table `insurance_statuses`
--

INSERT INTO `insurance_statuses` (`id`, `insurance_status_name`) VALUES
(1, 'Insured'),
(2, 'Not Insured');

-- --------------------------------------------------------

--
-- Table structure for table `machines`
--

CREATE TABLE `machines` (
  `machine_id` int(11) NOT NULL,
  `machine_name` varchar(200) COLLATE utf8_unicode_520_ci NOT NULL,
  `major_functions` int(11) NOT NULL,
  `machine_model` varchar(30) COLLATE utf8_unicode_520_ci NOT NULL,
  `machine_product_unit` varchar(50) COLLATE utf8_unicode_520_ci NOT NULL DEFAULT 'km per hour',
  `machine_capacity` int(11) NOT NULL,
  `machine_group` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_520_ci;

--
-- Dumping data for table `machines`
--

INSERT INTO `machines` (`machine_id`, `machine_name`, `major_functions`, `machine_model`, `machine_product_unit`, `machine_capacity`, `machine_group`) VALUES
(1, 'Big Mixer-750L-diesel', 1, 'BIG', 'Hours', 750, 1),
(2, 'BIG Mixer-750-Electrical', 1, 'big', 'hours', 750, 1),
(3, 'SMALL MIXER-350L-Electrical', 1, 'small', 'hours', 350, 1),
(4, 'SMALL MIXER-350L-Diesel', 1, 'small', 'hours', 350, 1),
(5, 'VIBRATOR ENGINE', 1, '', 'hours', 0, 1),
(6, 'VIBRATOR HOSE', 1, '', 'hours', 0, 1),
(7, 'BATCHING PLANT', 1, '', 'hours', 0, 2),
(8, 'TRUCKMIXER', 1, 'ZZ1257N4048W', 'hours', 9, 2),
(9, 'NEW TRUCKMIXER', 1, 'SY309C', 'hours', 9, 2),
(10, 'KOBELKO CRANE', 1, 'RR70M', 'hours', 0, 2),
(11, 'FIORI', 1, 'DB-260', 'hours', 2, 2),
(12, 'FIORI 2', 1, 'DB460', 'hours', 3, 2),
(13, 'CONCRETE PUMP new', 1, 'SANY HBT6013C-5D', 'hours', 9, 2),
(14, 'CONCRETE PUMP ', 1, 'SANY', 'hours', 9, 2),
(15, 'Dozer', 2, 'DZ-117 ', 'hours', 158, 2),
(16, 'LOADER', 2, 'SIEM 659B', 'hours', 3, 2),
(17, 'LOADER 2', 2, 'XCMG', 'hours', 3, 2),
(18, 'JACK HAMMER', 2, '', 'hours', 0, 2),
(19, 'POWER PLUS GRADER', 2, 'PP14G-VI', 'hours', 0, 2),
(20, 'POWER PLUS ROLLER', 2, 'CV302PD3', 'hours', 0, 2),
(21, 'BACK HOE LOADER CAT', 2, '420D', 'hours', 1, 2),
(22, 'BACK HOE LOADER CAT 2', 2, '428B', 'hours', 1, 2);

-- --------------------------------------------------------

--
-- Table structure for table `machine_functions`
--

CREATE TABLE `machine_functions` (
  `id` int(11) NOT NULL,
  `machine_function_name` varchar(50) COLLATE utf8_unicode_520_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_520_ci;

--
-- Dumping data for table `machine_functions`
--

INSERT INTO `machine_functions` (`id`, `machine_function_name`) VALUES
(1, 'Concreting equipment'),
(2, 'Earth work equipment'),
(3, ' Construction Vehicle'),
(4, 'Laboratory equipment'),
(5, 'Material hoisting equipment'),
(6, 'Rebar cutter and Aluminum eqt'),
(7, 'Support and Utility service'),
(8, 'Vehicles'),
(9, 'other');

-- --------------------------------------------------------

--
-- Table structure for table `machine_groups`
--

CREATE TABLE `machine_groups` (
  `machine_group_id` int(11) NOT NULL,
  `machine_group_name` varchar(50) COLLATE utf8_unicode_520_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_520_ci;

--
-- Dumping data for table `machine_groups`
--

INSERT INTO `machine_groups` (`machine_group_id`, `machine_group_name`) VALUES
(1, 'Light duty machine'),
(2, 'Heavy duty machine'),
(3, 'Small Vehicles'),
(4, 'Trucks'),
(5, 'Others');

-- --------------------------------------------------------

--
-- Table structure for table `maintainance_priority`
--

CREATE TABLE `maintainance_priority` (
  `id` int(11) NOT NULL,
  `priority_name` varchar(20) COLLATE utf8_unicode_520_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_520_ci;

--
-- Dumping data for table `maintainance_priority`
--

INSERT INTO `maintainance_priority` (`id`, `priority_name`) VALUES
(1, 'Low'),
(2, 'Medium'),
(3, 'High');

-- --------------------------------------------------------

--
-- Table structure for table `maintainance_request`
--

CREATE TABLE `maintainance_request` (
  `maintainance_id` int(11) NOT NULL,
  `equipment_id` varchar(50) COLLATE utf8_unicode_520_ci NOT NULL,
  `maintainance_request_date` date NOT NULL,
  `maintainance_requested_completion` date NOT NULL,
  `maintainance_problem` varchar(1000) COLLATE utf8_unicode_520_ci NOT NULL,
  `requested_by` varchar(15) COLLATE utf8_unicode_520_ci NOT NULL,
  `request_status` int(11) NOT NULL DEFAULT '1',
  `em_approved` tinyint(1) NOT NULL DEFAULT '0',
  `work_order_Id` varchar(25) COLLATE utf8_unicode_520_ci DEFAULT NULL,
  `maintainance_sparepart_cost` int(11) DEFAULT NULL,
  `maintainance_labor_cost` int(11) DEFAULT NULL,
  `maintainance_status` int(11) NOT NULL DEFAULT '1',
  `maintainance_priority` int(11) DEFAULT NULL,
  `maintainance_completion_date` date DEFAULT NULL,
  `om_approved` tinyint(1) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_520_ci;

-- --------------------------------------------------------

--
-- Table structure for table `maintainance_status`
--

CREATE TABLE `maintainance_status` (
  `id` int(11) NOT NULL,
  `maintainance_status` varchar(20) COLLATE utf8_unicode_520_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_520_ci;

--
-- Dumping data for table `maintainance_status`
--

INSERT INTO `maintainance_status` (`id`, `maintainance_status`) VALUES
(1, 'Pending'),
(2, 'On-Progress'),
(3, 'Completed');

-- --------------------------------------------------------

--
-- Table structure for table `project_list`
--

CREATE TABLE `project_list` (
  `id` int(11) NOT NULL,
  `Code` varchar(5) COLLATE utf8_unicode_520_ci NOT NULL,
  `Name` varchar(30) COLLATE utf8_unicode_520_ci NOT NULL,
  `Status` varchar(15) COLLATE utf8_unicode_520_ci NOT NULL DEFAULT 'On Progress'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_520_ci;

-- --------------------------------------------------------

--
-- Table structure for table `request_status`
--

CREATE TABLE `request_status` (
  `id` int(11) NOT NULL,
  `name` varchar(15) COLLATE utf8_unicode_520_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_520_ci;

--
-- Dumping data for table `request_status`
--

INSERT INTO `request_status` (`id`, `name`) VALUES
(1, 'unseen'),
(2, 'seen'),
(3, 'approved'),
(4, 'Discarded'),
(5, 'completed');

-- --------------------------------------------------------

--
-- Table structure for table `service_request`
--

CREATE TABLE `service_request` (
  `id` int(11) NOT NULL,
  `equipment_id` varchar(50) COLLATE utf8_unicode_520_ci NOT NULL,
  `request_date` date NOT NULL,
  `requested_by` varchar(15) COLLATE utf8_unicode_520_ci NOT NULL,
  `service_status` int(11) NOT NULL DEFAULT '1',
  `em_approved` tinyint(1) NOT NULL,
  `om_approved` tinyint(1) NOT NULL,
  `service_completion_date` date NOT NULL,
  `service_labor_cost` decimal(10,0) NOT NULL,
  `service_sparepart_cost` decimal(10,0) NOT NULL,
  `serviced_at` decimal(10,0) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_520_ci;

-- --------------------------------------------------------

--
-- Table structure for table `sparepart_purchase`
--

CREATE TABLE `sparepart_purchase` (
  `id` int(11) NOT NULL,
  `work_id` varchar(25) COLLATE utf8_unicode_520_ci NOT NULL,
  `item_description` varchar(500) COLLATE utf8_unicode_520_ci NOT NULL,
  `Unit` varchar(15) COLLATE utf8_unicode_520_ci NOT NULL,
  `qty` int(11) NOT NULL,
  `om_approved` tinyint(1) NOT NULL DEFAULT '0',
  `date_purchased` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_520_ci;

-- --------------------------------------------------------

--
-- Table structure for table `supplier_info`
--

CREATE TABLE `supplier_info` (
  `supplier_id` int(11) NOT NULL,
  `supplier_name` varchar(100) COLLATE utf8_unicode_520_ci NOT NULL,
  `supplier_address` varchar(200) COLLATE utf8_unicode_520_ci NOT NULL,
  `supplier_phone1` varchar(15) COLLATE utf8_unicode_520_ci NOT NULL,
  `supplier_phone2` varchar(15) COLLATE utf8_unicode_520_ci NOT NULL,
  `supplier_po_box` varchar(15) COLLATE utf8_unicode_520_ci NOT NULL,
  `supplier_email` varchar(50) COLLATE utf8_unicode_520_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_520_ci;

-- --------------------------------------------------------

--
-- Table structure for table `transact_history`
--

CREATE TABLE `transact_history` (
  `transact_id` int(11) NOT NULL,
  `transact_time` datetime NOT NULL,
  `transact_summary` varchar(100) COLLATE utf8_unicode_520_ci NOT NULL,
  `userid` varchar(15) COLLATE utf8_unicode_520_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_520_ci;

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `user_id` varchar(15) COLLATE utf8_unicode_520_ci NOT NULL,
  `user_name` varchar(25) COLLATE utf8_unicode_520_ci NOT NULL,
  `user_fathername` varchar(25) COLLATE utf8_unicode_520_ci NOT NULL,
  `user_department` int(11) NOT NULL,
  `user_location` int(11) NOT NULL,
  `user_email` varchar(50) COLLATE utf8_unicode_520_ci NOT NULL,
  `user_phone` bigint(13) NOT NULL,
  `user_password` varchar(32) COLLATE utf8_unicode_520_ci NOT NULL,
  `user_level` int(11) NOT NULL,
  `user_status` tinyint(1) NOT NULL DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_520_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`user_id`, `user_name`, `user_fathername`, `user_department`, `user_location`, `user_email`, `user_phone`, `user_password`, `user_level`, `user_status`) VALUES
('AD-000', 'System', 'Admin', 6, 51, 'ok', 973111473, '789456123', 7, 0),
('FL-0001', 'Abiy', 'Melaku', 4, 51, 'abmlk@gmail.com', 251920320186, 'ab@123', 5, 1),
('MK200', 'Samuel', 'Rahimeto', 7, 43, 'samirahmt', 973111473, '0973111473', 7, 0);

-- --------------------------------------------------------

--
-- Table structure for table `user_levels`
--

CREATE TABLE `user_levels` (
  `id` int(11) NOT NULL,
  `user_level_name` varchar(150) COLLATE utf8_unicode_520_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_520_ci;

--
-- Dumping data for table `user_levels`
--

INSERT INTO `user_levels` (`id`, `user_level_name`) VALUES
(1, 'Senior Mechanic'),
(2, 'Site Engineer'),
(3, 'Project Manager'),
(4, 'Equipment Manager'),
(5, 'Operation Manager'),
(7, 'System Admin'),
(8, 'Equipment Foreman');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `allocate_item`
--
ALTER TABLE `allocate_item`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `equipment_id_2` (`equipment_id`,`allocate_to`),
  ADD KEY `equipment_id` (`equipment_id`);

--
-- Indexes for table `allocation_request`
--
ALTER TABLE `allocation_request`
  ADD PRIMARY KEY (`id`),
  ADD KEY `machine_id` (`machine_id`),
  ADD KEY `site_id` (`site_id`),
  ADD KEY `request_status` (`request_status`),
  ADD KEY `requested_by` (`requested_by`);

--
-- Indexes for table `department`
--
ALTER TABLE `department`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `equipment_list`
--
ALTER TABLE `equipment_list`
  ADD PRIMARY KEY (`equipment_id`),
  ADD KEY `current_status` (`current_status`),
  ADD KEY `machine_id` (`machine_id`),
  ADD KEY `current_location` (`current_location`),
  ADD KEY `equipment_type` (`equipment_type`),
  ADD KEY `operator_id` (`operator_id`),
  ADD KEY `insurance_status` (`insurance_status`);

--
-- Indexes for table `equipment_productivity_control`
--
ALTER TABLE `equipment_productivity_control`
  ADD PRIMARY KEY (`id`),
  ADD KEY `user_id` (`user_id`),
  ADD KEY `equipment_id` (`equipment_id`);

--
-- Indexes for table `equipment_purchase_request`
--
ALTER TABLE `equipment_purchase_request`
  ADD PRIMARY KEY (`id`),
  ADD KEY `machine_id` (`machine_id`),
  ADD KEY `site_id` (`site_id`);

--
-- Indexes for table `equipment_status`
--
ALTER TABLE `equipment_status`
  ADD PRIMARY KEY (`status_id`);

--
-- Indexes for table `equipment_type`
--
ALTER TABLE `equipment_type`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `insurance_payment`
--
ALTER TABLE `insurance_payment`
  ADD PRIMARY KEY (`id`),
  ADD KEY `equipment_id` (`equipment_id`);

--
-- Indexes for table `insurance_statuses`
--
ALTER TABLE `insurance_statuses`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `machines`
--
ALTER TABLE `machines`
  ADD PRIMARY KEY (`machine_id`),
  ADD KEY `major_functions` (`major_functions`),
  ADD KEY `machin_group` (`machine_group`);

--
-- Indexes for table `machine_functions`
--
ALTER TABLE `machine_functions`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `machine_groups`
--
ALTER TABLE `machine_groups`
  ADD PRIMARY KEY (`machine_group_id`);

--
-- Indexes for table `maintainance_priority`
--
ALTER TABLE `maintainance_priority`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `maintainance_request`
--
ALTER TABLE `maintainance_request`
  ADD PRIMARY KEY (`maintainance_id`),
  ADD UNIQUE KEY `work_order_Id_2` (`work_order_Id`),
  ADD KEY `equipment_id` (`equipment_id`),
  ADD KEY `inspecter_id` (`requested_by`),
  ADD KEY `maintainance_priority` (`maintainance_priority`),
  ADD KEY `maintainance_status` (`maintainance_status`),
  ADD KEY `request_status` (`request_status`),
  ADD KEY `work_order_Id` (`work_order_Id`);

--
-- Indexes for table `maintainance_status`
--
ALTER TABLE `maintainance_status`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `project_list`
--
ALTER TABLE `project_list`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `request_status`
--
ALTER TABLE `request_status`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `service_request`
--
ALTER TABLE `service_request`
  ADD PRIMARY KEY (`id`),
  ADD KEY `equipment_id` (`equipment_id`),
  ADD KEY `requested_by` (`requested_by`),
  ADD KEY `service_status` (`service_status`);

--
-- Indexes for table `sparepart_purchase`
--
ALTER TABLE `sparepart_purchase`
  ADD PRIMARY KEY (`id`),
  ADD KEY `work_id` (`work_id`);

--
-- Indexes for table `supplier_info`
--
ALTER TABLE `supplier_info`
  ADD PRIMARY KEY (`supplier_id`);

--
-- Indexes for table `transact_history`
--
ALTER TABLE `transact_history`
  ADD PRIMARY KEY (`transact_id`),
  ADD KEY `userid` (`userid`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`user_id`),
  ADD UNIQUE KEY `user_email` (`user_email`),
  ADD KEY `user_department` (`user_department`),
  ADD KEY `user_location` (`user_location`),
  ADD KEY `user_level` (`user_level`),
  ADD KEY `user_status` (`user_status`),
  ADD KEY `user_level_2` (`user_level`);

--
-- Indexes for table `user_levels`
--
ALTER TABLE `user_levels`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `allocate_item`
--
ALTER TABLE `allocate_item`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `allocation_request`
--
ALTER TABLE `allocation_request`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `department`
--
ALTER TABLE `department`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `equipment_productivity_control`
--
ALTER TABLE `equipment_productivity_control`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `equipment_purchase_request`
--
ALTER TABLE `equipment_purchase_request`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `equipment_status`
--
ALTER TABLE `equipment_status`
  MODIFY `status_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `equipment_type`
--
ALTER TABLE `equipment_type`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `insurance_payment`
--
ALTER TABLE `insurance_payment`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `insurance_statuses`
--
ALTER TABLE `insurance_statuses`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `machines`
--
ALTER TABLE `machines`
  MODIFY `machine_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=23;

--
-- AUTO_INCREMENT for table `machine_functions`
--
ALTER TABLE `machine_functions`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `machine_groups`
--
ALTER TABLE `machine_groups`
  MODIFY `machine_group_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `maintainance_priority`
--
ALTER TABLE `maintainance_priority`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `maintainance_request`
--
ALTER TABLE `maintainance_request`
  MODIFY `maintainance_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `maintainance_status`
--
ALTER TABLE `maintainance_status`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `project_list`
--
ALTER TABLE `project_list`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `request_status`
--
ALTER TABLE `request_status`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `service_request`
--
ALTER TABLE `service_request`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `sparepart_purchase`
--
ALTER TABLE `sparepart_purchase`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `supplier_info`
--
ALTER TABLE `supplier_info`
  MODIFY `supplier_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `transact_history`
--
ALTER TABLE `transact_history`
  MODIFY `transact_id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `user_levels`
--
ALTER TABLE `user_levels`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `allocation_request`
--
ALTER TABLE `allocation_request`
  ADD CONSTRAINT `allocation_request_ibfk_1` FOREIGN KEY (`machine_id`) REFERENCES `machines` (`machine_id`),
  ADD CONSTRAINT `allocation_request_ibfk_2` FOREIGN KEY (`site_id`) REFERENCES `project_list` (`id`),
  ADD CONSTRAINT `allocation_request_ibfk_3` FOREIGN KEY (`request_status`) REFERENCES `request_status` (`id`),
  ADD CONSTRAINT `allocation_request_ibfk_4` FOREIGN KEY (`requested_by`) REFERENCES `users` (`user_id`);

--
-- Constraints for table `equipment_list`
--
ALTER TABLE `equipment_list`
  ADD CONSTRAINT `equipment_list_ibfk_1` FOREIGN KEY (`machine_id`) REFERENCES `machines` (`machine_id`),
  ADD CONSTRAINT `equipment_list_ibfk_2` FOREIGN KEY (`current_location`) REFERENCES `project_list` (`id`),
  ADD CONSTRAINT `equipment_list_ibfk_3` FOREIGN KEY (`current_status`) REFERENCES `equipment_status` (`status_id`),
  ADD CONSTRAINT `equipment_list_ibfk_4` FOREIGN KEY (`equipment_type`) REFERENCES `equipment_type` (`id`),
  ADD CONSTRAINT `equipment_list_ibfk_5` FOREIGN KEY (`insurance_status`) REFERENCES `insurance_statuses` (`id`);

--
-- Constraints for table `equipment_productivity_control`
--
ALTER TABLE `equipment_productivity_control`
  ADD CONSTRAINT `equipment_productivity_control_ibfk_1` FOREIGN KEY (`equipment_id`) REFERENCES `equipment_list` (`equipment_id`),
  ADD CONSTRAINT `equipment_productivity_control_ibfk_2` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`);

--
-- Constraints for table `equipment_purchase_request`
--
ALTER TABLE `equipment_purchase_request`
  ADD CONSTRAINT `equipment_purchase_request_ibfk_1` FOREIGN KEY (`machine_id`) REFERENCES `machines` (`machine_id`),
  ADD CONSTRAINT `equipment_purchase_request_ibfk_2` FOREIGN KEY (`site_id`) REFERENCES `project_list` (`id`);

--
-- Constraints for table `insurance_payment`
--
ALTER TABLE `insurance_payment`
  ADD CONSTRAINT `insurance_payment_ibfk_1` FOREIGN KEY (`equipment_id`) REFERENCES `equipment_list` (`equipment_id`);

--
-- Constraints for table `machines`
--
ALTER TABLE `machines`
  ADD CONSTRAINT `machines_ibfk_1` FOREIGN KEY (`major_functions`) REFERENCES `machine_functions` (`id`),
  ADD CONSTRAINT `machines_ibfk_2` FOREIGN KEY (`machine_group`) REFERENCES `machine_groups` (`machine_group_id`);

--
-- Constraints for table `maintainance_request`
--
ALTER TABLE `maintainance_request`
  ADD CONSTRAINT `maintainance_request_ibfk_1` FOREIGN KEY (`requested_by`) REFERENCES `users` (`user_id`),
  ADD CONSTRAINT `maintainance_request_ibfk_2` FOREIGN KEY (`equipment_id`) REFERENCES `equipment_list` (`equipment_id`),
  ADD CONSTRAINT `maintainance_request_ibfk_4` FOREIGN KEY (`maintainance_priority`) REFERENCES `maintainance_priority` (`id`),
  ADD CONSTRAINT `maintainance_request_ibfk_5` FOREIGN KEY (`request_status`) REFERENCES `request_status` (`id`);

--
-- Constraints for table `service_request`
--
ALTER TABLE `service_request`
  ADD CONSTRAINT `service_request_ibfk_1` FOREIGN KEY (`service_status`) REFERENCES `maintainance_status` (`id`),
  ADD CONSTRAINT `service_request_ibfk_2` FOREIGN KEY (`equipment_id`) REFERENCES `equipment_list` (`equipment_id`),
  ADD CONSTRAINT `service_request_ibfk_3` FOREIGN KEY (`requested_by`) REFERENCES `users` (`user_id`);

--
-- Constraints for table `transact_history`
--
ALTER TABLE `transact_history`
  ADD CONSTRAINT `transact_history_ibfk_1` FOREIGN KEY (`userid`) REFERENCES `users` (`user_id`);

--
-- Constraints for table `users`
--
ALTER TABLE `users`
  ADD CONSTRAINT `users_ibfk_1` FOREIGN KEY (`user_level`) REFERENCES `user_levels` (`id`),
  ADD CONSTRAINT `users_ibfk_2` FOREIGN KEY (`user_department`) REFERENCES `department` (`id`),
  ADD CONSTRAINT `users_ibfk_3` FOREIGN KEY (`user_location`) REFERENCES `project_list` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;

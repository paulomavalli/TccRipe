USE collateral;

CREATE TABLE collateral_consumer_type (
  consumer_type_id int NOT NULL AUTO_INCREMENT, -- ID do Tipo do Empréstimo ou produto que bloqueia a garantia
  consumer_type varchar(45) NOT NULL, -- Tipo do Empréstimo ou produto que bloqueia a garantia
  creation_date timestamp NOT NULL DEFAULT current_timestamp, -- Data de inclusão
  update_date timestamp NOT NULL DEFAULT current_timestamp, -- Data de modificação
  PRIMARY KEY (consumer_type_id) -- Otimizar a obtenção do tipo do consumidor
);

USE collateral;
CREATE TABLE collateral (
  collateral_id varchar(45) NOT NULL, -- GUID da Garantia
  consumer_id varchar(45) NOT NULL, -- ID do Empréstimo ou produto que bloqueia a garantia
  consumer_type_id int NOT NULL, -- Tipo do Empréstimo ou produto que bloqueia a garantia
  security_id varchar(45) NOT NULL, -- ID do ativo/investimento/custódia em garantia
  security_type varchar(12) NOT NULL, -- Tipo do ativo/investimento/custódia em garantia
  quantity decimal(18,2) NOT NULL, -- Quantidade do ativo/investimento/custódia em garantia
  customer_id varchar(12) NOT NULL, -- CPF do cliente
  creation_date timestamp NOT NULL DEFAULT current_timestamp, -- Data de inclusão da garantia
  update_date timestamp NOT NULL DEFAULT current_timestamp, -- Data de modificação da garantia
  is_active tinyint NOT NULL DEFAULT 1, -- Status binário da garantia 1 = em uso
  PRIMARY KEY (collateral_id), -- Otimizar a obtenção da garantia
  CONSTRAINT fk_colllateral_consumer_type
    FOREIGN KEY (consumer_type_id)
    REFERENCES collateral_consumer_type (consumer_type_id)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION
);

USE collateral;
USE collateral;
CREATE TABLE collateral_priority (
  product_type_id varchar(18) NOT NULL, -- Tipo do ativo/investimento/custódia ou produto 
  product_type_description varchar(45) NOT NULL, -- descrição do ativo/investimento/custódia em garantia
  priority_scale int NOT NULL, -- número correspondente a prioridade
  priority_scale_description varchar(12) NOT NULL, -- descrição da prioridade
  creation_date timestamp NOT NULL DEFAULT current_timestamp, -- Data de inclusão da garantia
  update_date timestamp NOT NULL DEFAULT current_timestamp, -- Data de modificação da garantia
  PRIMARY KEY (product_type_id,product_type_description) -- primary key composta
  );
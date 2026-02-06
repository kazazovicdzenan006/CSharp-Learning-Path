/* SQL TRAINING: FROM NORMALIZATION TO COMPLEX QUERIES
   Description: Practice covering Database Normalization (1NF-3NF), 
   DML operations (Insert, Update, Delete), and advanced SQL retrieval.
*/

/* DATABASE SCHEMA :
   1. Authors (author_id, first_name, last_name, country)
   2. Books (book_id, title, author_id, price, genre)
   3. Sales (sale_id, book_id, sale_date, quantity, customer_city)
*/


--PART 1: DATABASE NORMALIZATION (1NF - 3NF)


/* Scenario: Student_Courses (0NF -> 3NF)
   Key insight: Eliminating partial and transitive dependencies.
*/

-- 1NF: Atomic values and Composite Primary Key
-- Table: Students_Courses_1NF (student_id, course_id, student_name, student_surname, course_name, grade, university_name, university_address)

-- 2NF: Removing Partial Dependencies (Separating entities)
-- Table: Students (student_id [PK], student_name, student_surname, university_name, university_address)
-- Table: Courses (course_id [PK], course_name)
-- Table: Enrollments (student_id [FK], course_id [FK], grade)

-- 3NF: Removing Transitive Dependencies (University name determines address)
-- Table: Universities (university_name [PK], university_address)
-- Table: Students (student_id [PK], student_name, student_surname, university_name [FK])



-- PART 2: DATA MANIPULATION (DML)


-- 1. Insert a new record with explicit columns
INSERT INTO Authors (first_name, last_name, country) 
VALUES ('Ivo', 'Andric', 'BiH');

-- 2. Update price for specific genre and range
UPDATE Books 
SET price = price + 5 
WHERE genre = 'Classics' AND price BETWEEN 15 AND 30;

-- 3. Delete records based on multiple conditions
DELETE FROM Sales 
WHERE customer_city = 'Mostar' AND quantity < 2;


-- PART 3: DATA RETRIEVAL (SELECT)


-- 4. Filtering with LIKE and logical NOT IN
SELECT first_name, last_name 
FROM Authors 
WHERE last_name LIKE 'M%' AND country NOT IN ('Serbia', 'Croatia');

-- 5. Aggregate COUNT with HAVING clause
SELECT country, COUNT(author_id) AS total_authors 
FROM Authors 
GROUP BY country 
HAVING COUNT(author_id) > 2;

-- 6. Average price per Author (JOIN + GROUP BY)
SELECT a.first_name, a.last_name, AVG(b.price) AS average_price 
FROM Authors a 
INNER JOIN Books b ON a.author_id = b.author_id 
GROUP BY a.first_name, a.last_name;

-- 7. Subquery: Find authors from the same country as author_id 5
SELECT first_name, last_name 
FROM Authors 
WHERE country = (SELECT country FROM Authors WHERE author_id = 5);

-- 8. Left Join: List all books and their authors (including those without authors)
SELECT b.title, a.first_name AS author_name 
FROM Books b 
LEFT JOIN Authors a ON b.author_id = a.author_id;

-- 9. Total Revenue by City (SUM + JOIN + HAVING)
SELECT s.customer_city, SUM(s.quantity * b.price) AS total_revenue 
FROM Sales s 
INNER JOIN Books b ON s.book_id = b.book_id 
GROUP BY s.customer_city 
HAVING SUM(s.quantity * b.price) > 5000;

-- 10. Distinct Genres sold in Sarajevo in 2025
SELECT DISTINCT b.genre 
FROM Books b 
INNER JOIN Sales s ON b.book_id = s.book_id 
WHERE s.customer_city = 'Sarajevo' 
  AND s.sale_date BETWEEN '2025-01-01' AND '2025-12-31' 
ORDER BY b.genre ASC;

-- 11. Find the book title with the MAX single-sale quantity (Subquery)
SELECT b.title, s.quantity 
FROM Books b 
INNER JOIN Sales s ON b.book_id = s.book_id 
WHERE s.quantity = (SELECT MAX(quantity) FROM Sales);

-- 12. Multiple Joins: Sales details with Book title and Author name
SELECT b.title, s.sale_date, a.last_name, s.quantity 
FROM Authors a 
INNER JOIN Books b ON a.author_id = b.author_id 
INNER JOIN Sales s ON b.book_id = s.book_id;

-- 13. Total quantity sold per genre in 2024
SELECT b.genre, SUM(s.quantity) AS total_sold 
FROM Sales s 
INNER JOIN Books b ON s.book_id = b.book_id 
WHERE s.sale_date BETWEEN '2024-01-01' AND '2024-12-31' 
GROUP BY b.genre;

-- 14. Complex Join & Aggregate: Total revenue by Author in Sarajevo (The most complex one)
SELECT a.first_name, a.last_name, SUM(s.quantity * b.price) AS total_revenue 
FROM Authors a 
INNER JOIN Books b ON a.author_id = b.author_id 
INNER JOIN Sales s ON b.book_id = s.book_id 
WHERE s.customer_city = 'Sarajevo' 
GROUP BY a.first_name, a.last_name 
ORDER BY total_revenue DESC 
LIMIT 1;

-- 15. Clean-up: Delete authors with no books assigned (Subquery)
DELETE FROM Authors 
WHERE author_id NOT IN (SELECT DISTINCT author_id FROM Books);
DO $$
DECLARE "Table" RECORD;
BEGIN
	FOR "Table" IN SELECT relname FROM pg_class WHERE relhastriggers AND NOT relname LIKE 'pg_%'
	AND relname IN (SELECT table_name FROM information_schema.tables WHERE table_schema = 'public')
	LOOP
	  EXECUTE 'ALTER TABLE "' || "Table".relname || '" DISABLE TRIGGER ALL';
	END LOOP;
END $$;

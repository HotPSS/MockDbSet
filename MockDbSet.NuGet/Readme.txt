An implementation of DbSet<T> that uses an in memory data source, to facilitate testing of Entity Framework dependent code.
Unlike some other libraries, it doesn't just implement IDbSet<T>, which could mean more code changes have to be done.

Much of the plumbing code is taken from the Entity Framework codebase itself and is copyright (c) Microsoft Open Technologies, Inc.
See "Entity Framework License.txt" for license details.

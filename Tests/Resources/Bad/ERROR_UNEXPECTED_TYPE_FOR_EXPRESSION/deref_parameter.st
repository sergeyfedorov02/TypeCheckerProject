// ERROR_NOT_A_REFERENCE
language core;
extend with #references;

fn foo(n : Nat) -> Nat { return *n }

fn main(n : Nat) -> Nat {
	return foo(n)
}

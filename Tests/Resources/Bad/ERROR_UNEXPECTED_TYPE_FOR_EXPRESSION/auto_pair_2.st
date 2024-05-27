language core;
extend with #type-reconstruction, #tuples, #multiparameter-functions;

fn do(a : Bool, b : Nat) -> auto {
	return { a, b }
}

fn main(n : auto) -> { Nat, Bool } {
	return do(Nat::iszero(n), n)
}
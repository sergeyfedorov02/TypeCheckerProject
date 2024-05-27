language core;
extend with #type-reconstruction, #tuples, #multiparameter-functions;

fn do(a : auto, b : auto) -> auto {
	return { a, b }
}

fn main(n : auto) -> { Bool, Nat } {
	return do(Nat::iszero(n), n)
}

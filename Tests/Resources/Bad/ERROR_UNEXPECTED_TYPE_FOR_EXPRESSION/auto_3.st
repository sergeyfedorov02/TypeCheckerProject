language core;
extend with #type-reconstruction;

fn do(n : Nat) -> auto {
	return Nat::iszero(n)
}

fn main(n : auto) -> Nat {
	return do(n)
}
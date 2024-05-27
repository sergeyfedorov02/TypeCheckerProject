language core;
extend with #type-reconstruction, #sum-types;

fn do(n : Nat) -> auto {
	return inl(n)
}

fn main(n : auto) -> Bool + Nat {
	return do(n)
}
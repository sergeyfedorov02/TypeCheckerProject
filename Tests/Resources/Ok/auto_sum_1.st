language core;
extend with #type-reconstruction, #sum-types;

fn do(n : Nat) -> auto {
	return inr(n)
}

fn main(n : auto) -> Bool + Nat {
	return do(n)
}
